using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class PlaylistDeletedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlist_deleted";

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
