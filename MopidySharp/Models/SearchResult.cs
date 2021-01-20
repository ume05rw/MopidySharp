using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// SearchResult Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.SearchResult
    /// </remarks>
    public class SearchResult
    {
        /// <summary>
        /// The albums matching the search query. Read-only.
        /// </summary>
        [JsonProperty("albums")]
        public Album[] Albums { get; set; }

        /// <summary>
        /// The artists matching the search query. Read-only.
        /// </summary>
        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        /// <summary>
        /// The tracks matching the search query. Read-only.
        /// </summary>
        [JsonProperty("tracks")]
        public Track[] Tracks { get; set; }

        /// <summary>
        /// The playlist URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
