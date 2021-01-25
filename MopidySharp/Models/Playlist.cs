using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mopidy.Models
{
    /// <summary>
    /// Playlist Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Playlist
    /// </remarks>
    public class Playlist
    {
        /// <summary>
        /// The playlist modification time in milliseconds since Unix epoch. Read-only.
        /// Integer, or None if unknown.
        /// </summary>
        [JsonProperty("last_modified")]
        public long? LastModified { get; set; }

        /// <summary>
        /// The number of tracks in the playlist. Read-only.
        /// </summary>
        [JsonProperty("length")]
        public int? Length { get; set; }

        /// <summary>
        /// The playlist name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The playlist’s tracks. Read-only.
        /// </summary>
        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; }

        /// <summary>
        /// The playlist URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
