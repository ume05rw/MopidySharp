using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for PlaylistDeleted
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlist_deleted
    /// </remarks>
    public class PlaylistDeletedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlist_deleted";

        /// <summary>
        /// the URI of the deleted playlist
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
