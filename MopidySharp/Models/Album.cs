using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// Album Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Album
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Album
    {
        /// <summary>
        /// A set of album artists. Read-only.
        /// </summary>
        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        /// <summary>
        /// The album release date. Read-only.
        /// </summary>
        [JsonProperty("Date")]
        public string Date { get; set; }

        /// <summary>
        /// The MusicBrainz ID of the album. Read-only.
        /// </summary>
        [JsonProperty("musicbrainz_id")]
        public string MusicbrainzId { get; set; }

        /// <summary>
        /// The album name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The number of discs in the album. Read-only.
        /// </summary>
        [JsonProperty("num_discs")]
        public int? NumDiscs { get; set; }

        /// <summary>
        /// The number of tracks in the album. Read-only.
        /// </summary>
        [JsonProperty("num_tracks")]
        public int? NumTracks { get; set; }

        /// <summary>
        /// The album URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// DUPLICATED: Image
        /// </summary>
        [JsonProperty("images")]
        public string[] Images { get; set; }
    }
}
