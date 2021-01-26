using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Core
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.Core
    /// </remarks>
    public static class Core
    {
        private const string MethodGetUriSchemes = "core.get_uri_schemes";
        private const string MethodGetVersion = "core.get_version";

        private static readonly Query _query = Query.Get();

        /// <summary>
        /// Get list of URI schemes we can handle
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, string[] Result)> GetUriSchemes()
        {
            var request = JsonRpcFactory.CreateRequest(Core.MethodGetUriSchemes);

            var response = await Core._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<string[]>();

            return (true, result);
        }

        /// <summary>
        /// Get version of the Mopidy core API
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, string Result)> GetVersion()
        {
            var request = JsonRpcFactory.CreateRequest(Core.MethodGetVersion);

            var response = await Core._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<string>();

            return (true, result);
        }
    }
}
