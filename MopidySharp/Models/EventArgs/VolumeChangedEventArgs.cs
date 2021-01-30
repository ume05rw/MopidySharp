using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.volume_changed
    /// </remarks>
    public class VolumeChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "volume_changed";

        /// <summary>
        /// the new volume in the range [0..100]
        /// </summary>
        [JsonProperty("volume")]
        public int Volume { get; set; }
    }
}
