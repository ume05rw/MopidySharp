using Newtonsoft.Json;

namespace Mopidy.Models
{
    /// <summary>
    /// Track Entity Model
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/models/#mopidy.models.Track
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class Track
    {
        /// <summary>
        /// The track Album. Read-only.
        /// </summary>
        [JsonProperty("album")]
        public Album Album { get; set; }

        /// <summary>
        /// A set of track artists. Read-only.
        /// </summary>
        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        /// <summary>
        /// The trackÅfs bitrate in kbit/s. Read-only.
        /// </summary>
        [JsonProperty("bitrate")]
        public int BitRate { get; set; }

        /// <summary>
        /// The track comment. Read-only.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// A set of track composers. Read-only.
        /// </summary>
        [JsonProperty("composers")]
        public Artist[] Composers { get; set; }

        /// <summary>
        /// The track release date. Read-only.
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// The disc number in the album. Read-only.
        /// </summary>
        [JsonProperty("disc_no")]
        public int? DiscNo { get; set; }

        /// <summary>
        /// The track genre. Read-only.
        /// </summary>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Integer representing when the track was last modified.
        /// Exact meaning depends on source of track.
        /// For local files this is the modification time in milliseconds since Unix epoch.
        /// For other backends it could be an equivalent timestamp or simply a version counter.
        /// </summary>
        [JsonProperty("last_modified")]
        public long? LastModified { get; set; }

        /// <summary>
        /// The track length in milliseconds. Read-only.
        /// </summary>
        [JsonProperty("length")]
        public int? Length { get; set; }

        /// <summary>
        /// The MusicBrainz ID of the track. Read-only.
        /// </summary>
        [JsonProperty("musicbrainz_id")]
        public string MusicbrainzId { get; set; }

        /// <summary>
        /// The track name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// A set of track performers`. Read-only.
        /// </summary>
        [JsonProperty("performers")]
        public Artist[] Performers { get; set; }

        /// <summary>
        /// The track number in the album. Read-only.
        /// </summary>
        [JsonProperty("track_no")]
        public int? TrackNo { get; set; }

        /// <summary>
        /// The track URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
