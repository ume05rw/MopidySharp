using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for MuteChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.mute_changed
    /// </remarks>
    public class MuteChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "mute_changed";

        /// <summary>
        /// the new mute state
        /// </summary>
        [JsonProperty("mute")]
        public bool Mute { get; set; }
    }
}
