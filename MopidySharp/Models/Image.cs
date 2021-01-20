using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// Image Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Image
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Image
    {
        /// <summary>
        /// Optional height of the image or None. Read-only.
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; set; }

        /// <summary>
        /// The image URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Optional width of the image or None. Read-only.
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }
    }
}
