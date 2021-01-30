using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for TrackPlaybackPaused
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_paused
    /// </remarks>
    public class TrackPlaybackPausedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_paused";

        /// <summary>
        /// the track that was playing when playback paused
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
