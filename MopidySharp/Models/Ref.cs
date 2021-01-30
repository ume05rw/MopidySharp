using Mopidy.Models.Enums;
using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// Ref Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Ref
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Ref
    {
        /// <summary>
        /// The object name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The object type, e.g. “artist”, “album”, “track”, “playlist”, “directory”. Read-only.
        /// </summary>
        [JsonProperty("type")]
        public RefType Type { get; set; }

        /// <summary>
        /// The object URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
