using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// Artist Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Artist
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Artist
    {
        /// <summary>
        /// The MusicBrainz ID of the artist. Read-only.
        /// </summary>
        [JsonProperty("musicbrainz_id")]
        public string MusicbrainzId { get; set; }

        /// <summary>
        /// The artist name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Artist name for better sorting, e.g. with articles stripped
        /// </summary>
        [JsonProperty("sortname")]
        public string SortName { get; set; }

        /// <summary>
        /// The artist URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
