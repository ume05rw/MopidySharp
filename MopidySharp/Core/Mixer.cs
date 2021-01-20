using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Mixer controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mixer-controller
    /// </remarks>
    public static class Mixer
    {
        private const string MethodGetMute = "core.mixer.get_mute";
        private const string MethodSetMute = "core.mixer.set_mute";
        private const string MethodGetVolume = "core.mixer.get_volume";
        private const string MethodSetVolume = "core.mixer.set_volume";

        private static readonly Query _query = Query.Get();

        /// <summary>
        /// Get mute state.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// True if muted, False unmuted, None if unknown.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> GetMute()
        {
            var request = JsonRpcFactory.CreateRequest(Mixer.MethodGetMute);

            var response = await Mixer._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, false);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<bool>();

            return (true, result);
        }

        /// <summary>
        /// Set mute state.
        /// </summary>
        /// <param name="mute">True to mute, False to unmute.</param>
        /// <returns></returns>
        /// <remarks>
        /// Returns True if call is successful, otherwise False.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> SetMute(
            bool mute
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Mixer.MethodSetMute,
                new
                {
                    mute
                }
            );

            var response = await Mixer._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, false);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<bool>();

            return (true, result);
        }

        /// <summary>
        /// Get the volume.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Integer in range [0..100] or None if unknown.
        /// The volume scale is linear.
        /// </remarks>
        public static async Task<(bool Succeeded, int? Result)> GetVolume()
        {
            var request = JsonRpcFactory.CreateRequest(Mixer.MethodGetVolume);

            var response = await Mixer._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, -1);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<int?>();

            return (true, result);
        }

        /// <summary>
        /// Set the volume.
        /// </summary>
        /// <param name="volume">The volume is defined as an integer in range [0..100].</param>
        /// <returns></returns>
        /// <remarks>
        /// The volume scale is linear.
        /// Returns True if call is successful, otherwise False.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> SetVolume(
            int volume
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Mixer.MethodSetVolume,
                new
                {
                    volume
                }
            );

            var response = await Mixer._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, false);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<bool>();

            return (true, result);
        }
    }
}
