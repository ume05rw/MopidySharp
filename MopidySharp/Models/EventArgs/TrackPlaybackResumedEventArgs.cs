using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for TrackPlaybackResumed
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_resumed
    /// </remarks>
    public class TrackPlaybackResumedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_resumed";

        /// <summary>
        /// the track that was playing when playback resumed
        /// </summary>
        [JsonProperty("tl_track")]
        public TlTrack TlTrack { get; set; }

        /// <summary>
        /// the time position in milliseconds
        /// </summary>
        [JsonProperty("time_position")]
        public int TimePosition { get; set; }
    }
}
