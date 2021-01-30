using Mopidy.Core;
using Mopidy.Models.EventArgs.Interfaces;
using Mopidy.Models.JsonRpcs.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mopidy.Models.JsonRpcs
{
    internal class QueryWebSocket : IQuery
    {
        internal static QueryWebSocket _instance = null;

        internal static QueryWebSocket Get()
        {
            if (QueryWebSocket._instance == null)
                QueryWebSocket._instance = new QueryWebSocket();

            return QueryWebSocket._instance;
        }


        private class ResponseSet
        {
            //public int Id { get; private set; }

            public Task<JsonRpcParamsResponse> ResponseTask { get; private set; }

            private JsonRpcParamsResponse response;

            public ResponseSet()
            {
                //this.Id = id;
                this.ResponseTask = new Task<JsonRpcParamsResponse>(() =>
                {
                    return this.response;
                });
            }

            public void CompleteTask(JsonRpcParamsResponse response)
            {
                this.response = response;
                this.ResponseTask.Start();

                _ = Task.Delay(1000)
                    .ContinueWith(t =>
                    {
                        this.Dispose();
                    });
            }

            public void Dispose()
            {
                try
                {
                    this.ResponseTask.Dispose();
                }
                catch (Exception)
                {
                }

                //this.Id = default;
                this.ResponseTask = null;
                this.response = null;
            }
        }


        private ConcurrentDictionary<int, ResponseSet> _responseDictionary;
        private const int MessageBufferSize = 8192;
        private ClientWebSocket _client;

        private QueryWebSocket()
        {
            this._client = null;
            this._responseDictionary = new ConcurrentDictionary<int, ResponseSet>();
        }

        private async Task<bool> Connect()
        {
            var url = Settings.WebSocketUrl;
            this._client = new ClientWebSocket();
            var uri = new Uri(url);

            try
            {
                await this._client.ConnectAsync(uri, CancellationToken.None);
            }
            catch (Exception ex)
            {
                this._client.Dispose();
                this._client = null;

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return false;
            }

            _ = Task.Run(this.RecieverLoop).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://qiita.com/Zumwalt/items/53797b0156ebbdcdbfb1
        /// </remarks>
        private void RecieverLoop()
        {
            if (this._client == null)
                throw new WebSocketException("Connection Not Ready.");

            _ = Task.Run(async () =>
            {
                var client = this._client;

                while (true)
                {
                    var buffer = new byte[QueryWebSocket.MessageBufferSize];
                    var segment = new ArraySegment<byte>(buffer);
                    var recieved = await client.ReceiveAsync(segment, CancellationToken.None);

                    //エンドポイントCloseの場合、処理を中断
                    if (recieved.MessageType == WebSocketMessageType.Close)
                    {
                        this._client = null;

                        await client.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            "OK",
                            CancellationToken.None
                        );

                        break;
                    }

                    //バイナリの場合は、当処理では扱えないため、処理を中断
                    if (recieved.MessageType == WebSocketMessageType.Binary)
                    {
                        this._client = null;

                        await client.CloseAsync(
                            WebSocketCloseStatus.InvalidMessageType,
                            "I don't do binary",
                            CancellationToken.None
                        );

                        break;
                    }

                    var result = new MemoryStream();
                    result.Write(buffer, 0, recieved.Count);

                    while (!recieved.EndOfMessage)
                    {
                        buffer = new byte[QueryWebSocket.MessageBufferSize];
                        segment = new ArraySegment<byte>(buffer);
                        recieved = await client.ReceiveAsync(
                            segment,
                            CancellationToken.None
                        );

                        result.Write(buffer, 0, recieved.Count);
                    }

                    this.ParseRecieved(result);
                }

                this._client = null;

                if (client.State != WebSocketState.Closed)
                {
                    try
                    {
                        await client.CloseAsync(
                            WebSocketCloseStatus.NormalClosure,
                            "OK",
                            CancellationToken.None
                        );
                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    client.Dispose();
                }
                catch (Exception)
                {
                }

                client = null;
            });
        }

        private void ParseRecieved(MemoryStream stream)
        {
            if (stream == null || stream.Length <= 0)
                return;

            var json = Encoding.UTF8.GetString(stream.ToArray());

            // TODO: イベントメッセージの形式を調べる。
            if (string.IsNullOrEmpty(json))
                return;

            IRecieved recieved;
            try
            {
                recieved = JsonConvert.DeserializeObject<IRecieved>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return;
            }

            if (recieved is JsonRpcParamsResponse)
            {
                var response = (JsonRpcParamsResponse)recieved;

                if (response == null || response.Id == null)
                    return;

                var id = (int)response.Id;
                if (this._responseDictionary.ContainsKey(id))
                {
                    var resSet = this._responseDictionary[id];
                    resSet.CompleteTask(response);

                    this._responseDictionary.TryRemove(id, out ResponseSet tmp);
                }
            }

            if (recieved is IEventArgs)
                CoreListener.FireEvent((IEventArgs)recieved);
        }

        public async Task<JsonRpcParamsResponse> Exec(JsonRpcQuery request)
        {
            if (this._client == null)
            {
                if (!await this.Connect())
                    throw new WebSocketException("Connection Refused.");
            }

            var isQuery = (request is JsonRpcQueryRequest);
            var resTask = default(Task<JsonRpcParamsResponse>);
            if (isQuery)
            {
                var id = ((JsonRpcQueryRequest)request).Id;
                var resSet = new ResponseSet();
                resTask = resSet.ResponseTask;

                if (!this._responseDictionary.TryAdd(id, resSet))
                    throw new ThreadStateException("ResponseSet Cannot Registered.");
            }
            else
            {
                resTask = Task<JsonRpcParamsResponse>.Run(() =>
                {
                    return new JsonRpcParamsResponse();
                });
            }

            try
            {
                var sendJson = JsonConvert.SerializeObject(request);
                var bytes = Encoding.UTF8.GetBytes(sendJson);
                var segment = new ArraySegment<byte>(bytes);

                await this._client.SendAsync(
                    segment,
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }
            catch (Exception ex)
            {
                var response = new JsonRpcParamsResponse()
                {
                    Error = ex
                };
                if (request is JsonRpcQueryRequest)
                    response.Id = ((JsonRpcQueryRequest)request).Id;

                return response;
            }

            return await resTask;
        }
    }
}
