using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class PlaylistChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlist_changed";

        [JsonProperty("playlist")]
        public Playlist Playlist { get; set; }
    }
}
