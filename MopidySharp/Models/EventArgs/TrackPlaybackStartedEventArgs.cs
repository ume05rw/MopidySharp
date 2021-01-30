using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class TrackPlaybackStartedEventArgs : EventArgsBase
    {
        internal const string EventName = "track_playback_started";

        [JsonProperty("tl_track ")]
        public TlTrack TlTrack { get; set; }
    }
}
