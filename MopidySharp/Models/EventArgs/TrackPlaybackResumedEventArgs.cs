using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class TrackPlaybackResumedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_resumed";

        [JsonProperty("tl_track ")]
        public TlTrack TlTrack { get; set; }

        [JsonProperty("time_position ")]
        public int TimePosition { get; set; }
    }
}
