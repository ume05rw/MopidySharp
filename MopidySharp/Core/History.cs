using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
using Mopidy.Models.JsonRpcs.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for History controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#history-controller
    /// </remarks>
    public static class History
    {
        private const string MethodGetHistory = "core.history.get_history";
        private const string MethodGetLength = "core.history.get_length";

        private static readonly IQuery _query = Query.Get();

        /// <summary>
        /// Get the track history.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The timestamps are milliseconds since epoch.
        /// </remarks>
        public static async Task<(bool Succeeded, Dictionary<long, Ref> Result)> GetHistory()
        {
            var request = JsonRpcFactory.CreateRequest(History.MethodGetHistory);

            var response = await History._query.Exec(request);

            if (response.Error != null)
                return (false, null);

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var array = JArray.FromObject(response.Result);
            var result = new Dictionary<long, Ref>();

            foreach (var child in array.Children())
            {
                if (child.Type != JTokenType.Array)
                    continue;

                long key = -1;
                Ref value = null;

                foreach (var elem in child.Children())
                {
                    if (elem.Type == JTokenType.Integer)
                        key = elem.Value<long>();
                    if (elem.Type == JTokenType.Object)
                        value = elem.ToObject<Ref>();
                }

                if (key != -1 && value != null)
                    result.Add(key, value);
            }

            return (true, result);
        }

        /// <summary>
        /// Get the number of tracks in the history.
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, int Result)> GetLength()
        {
            var request = JsonRpcFactory.CreateRequest(History.MethodGetLength);

            var response = await History._query.Exec(request);

            if (response.Error != null)
                return (false, -1);

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<int>();

            return (true, result);
        }
    }
}
