using Mopidy.Models.Enums;
using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for PlaybackStateChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playback_state_changed
    /// </remarks>
    public class PlaybackStateChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "playback_state_changed";

        /// <summary>
        /// the state before the change
        /// </summary>
        [JsonProperty("old_state")]
        public PlaybackState OldState { get; set; }

        /// <summary>
        /// the state after the change
        /// </summary>
        [JsonProperty("new_state")]
        public PlaybackState NewState { get; set; }
    }
}
