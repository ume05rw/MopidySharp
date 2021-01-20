using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
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

        private static readonly Query _query = Query.Get();

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
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // TODO: 注意、戻り値の型を確認する。
            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JObject.FromObject(response.Result)
                .ToObject<Dictionary<long, Ref>>();

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
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, -1);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<int>();

            return (true, result);
        }
    }
}
