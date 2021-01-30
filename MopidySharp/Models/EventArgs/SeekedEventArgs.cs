using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for Seeked
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.seeked
    /// </remarks>
    public class SeekedEventArgs : EventArgsBase
    {
        internal const string EventName = "seeked";

        /// <summary>
        /// the position that was seeked to in milliseconds
        /// </summary>
        [JsonProperty("time_position")]
        public int TimePosition { get; set; }
    }
}
