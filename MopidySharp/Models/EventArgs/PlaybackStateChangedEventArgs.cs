using Mopidy.Models.Enums;
using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class PlaybackStateChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "playback_state_changed";

        [JsonProperty("old_state")]
        public PlaybackState OldState { get; set; }

        [JsonProperty("new_state")]
        public PlaybackState NewState { get; set; }
    }
}
