using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for PlaylistChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlist_changed
    /// </remarks>
    public class PlaylistChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlist_changed";

        /// <summary>
        /// the changed playlist
        /// </summary>
        [JsonProperty("playlist")]
        public Playlist Playlist { get; set; }
    }
}
