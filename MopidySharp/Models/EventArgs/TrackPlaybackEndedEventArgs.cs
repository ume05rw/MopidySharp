using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for TrackPlaybackEnded
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_ended
    /// </remarks>
    public class TrackPlaybackEndedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_ended";

        /// <summary>
        /// the track that was played before playback stopped
        /// </summary>
        [JsonProperty("tl_track ")]
        public TlTrack TlTrack { get; set; }

        /// <summary>
        /// the time position in milliseconds
        /// </summary>
        [JsonProperty("time_position ")]
        public int TimePosition { get; set; }
    }
}
