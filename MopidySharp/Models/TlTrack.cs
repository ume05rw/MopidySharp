using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// TlTrack Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.TlTrack
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class TlTrack
    {
        /// <summary>
        /// The tracklist ID. Read-only.
        /// </summary>
        [JsonProperty("tlid")]
        public int TlId { get; set; }

        /// <summary>
        /// The track. Read-only.
        /// </summary>
        [JsonProperty("track")]
        public Track Track { get; set; }
    }
}
