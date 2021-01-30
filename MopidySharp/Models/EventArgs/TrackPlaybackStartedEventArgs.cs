using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for TrackPlaybackStarted
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_started
    /// </remarks>
    public class TrackPlaybackStartedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_started";

        /// <summary>
        /// the track that just started playing
        /// </summary>
        [JsonProperty("tl_track ")]
        public TlTrack TlTrack { get; set; }
    }
}
