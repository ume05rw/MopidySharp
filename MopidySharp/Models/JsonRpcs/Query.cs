using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mopidy.Models.JsonRpcs
{
    internal class Query
    {
        internal static Query _instance = null;
        internal static Query Get()
        {
            if (Query._instance == null)
                Query._instance = new Query();

            return Query._instance;
        }

        private Query()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// ここでの戻り値[JsonRpcParamsResponse]は直接通信で返すものではない。
        /// 通信応答を返す際に、ここの応答をJsonRpcFactory.CreateResultで整形して返す。
        /// </remarks>
        public async Task<JsonRpcParamsResponse> Exec(JsonRpcQuery request)
        {
            var url = Settings.RpcUrl;

            HttpResponseMessage message;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                client.Timeout = TimeSpan.FromMilliseconds(500000); // 500秒

                try
                {
                    var sendJson = JsonConvert.SerializeObject(request);
                    var content = new StringContent(sendJson, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    message = await client.PostAsync(url, content);
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

                var json = await message.Content.ReadAsStringAsync();

                if (json == null || string.IsNullOrEmpty(json))
                {
                    // 通知の時は応答JSON自体が無いことがある。
                    return null;
                }
                else
                {
                    var response = JsonConvert.DeserializeObject<JsonRpcParamsResponse>(json);
                    return response;
                }
            }
        }
    }
}
