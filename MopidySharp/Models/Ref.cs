using Newtonsoft.Json;
using System;

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
        internal const string TypeAlbum = "album";
        internal const string TypeArtist = "artist";
        internal const string TypeDirectory = "directory";
        internal const string TypePlaylist = "playlist";
        internal const string TypeTrack = "track";

        /// <summary>
        /// Type of Ref
        /// </summary>
        public enum RefType
        {
            /// <summary>
            /// Artist
            /// </summary>
            Artist,

            /// <summary>
            /// Album
            /// </summary>
            Album,

            /// <summary>
            /// Track
            /// </summary>
            Track,

            /// <summary>
            /// Playlist
            /// </summary>
            Playlist,

            /// <summary>
            /// Directory
            /// </summary>
            Directory
        }

        internal class RefTypeConverter: JsonConverter
        {
            public override bool CanWrite => true;
            public override bool CanRead => true;
            public override bool CanConvert(Type objectType)
                => (objectType == typeof(RefType));

            public override void WriteJson(
                JsonWriter writer,
                object value,
                JsonSerializer serializer
            )
            {
                RefType type;

                try
                {
                    type = (RefType)value;
                }
                catch (Exception ex)
                {
                    throw new JsonWriterException($"Unexpected Value[RefType]<{value}>: {ex.Message}", ex);
                }

                switch(type)
                {
                    case RefType.Album:
                        writer.WriteValue(Ref.TypeAlbum);
                        break;
                    case RefType.Artist:
                        writer.WriteValue(Ref.TypeArtist);
                        break;
                    case RefType.Directory:
                        writer.WriteValue(Ref.TypeDirectory);
                        break;
                    case RefType.Playlist:
                        writer.WriteValue(Ref.TypePlaylist);
                        break;
                    case RefType.Track:
                        writer.WriteValue(Ref.TypeTrack);
                        break;
                    default:
                        throw new JsonWriterException($"Unexpected Value[RefType]<{value}>");
                }
            }

            public override object ReadJson(
                JsonReader reader,
                Type objectType,
                object existingValue,
                JsonSerializer serializer
            )
            {
                if (reader.Value == null || string.IsNullOrEmpty(reader.Value.ToString()))
                    throw new JsonReaderException("Value[type] is Null or Empty.");

                var typeString = reader.Value.ToString();
                switch (typeString)
                {
                    case Ref.TypeAlbum:
                        return RefType.Album;
                    case Ref.TypeArtist:
                        return RefType.Artist;
                    case Ref.TypeDirectory:
                        return RefType.Directory;
                    case Ref.TypePlaylist:
                        return RefType.Playlist;
                    case Ref.TypeTrack:
                        return RefType.Track;
                    default:
                        throw new JsonReaderException($"Unexpected Value[type]<{typeString}>");
                }
            }
        }


        /// <summary>
        /// The object name. Read-only.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The object type, e.g. “artist”, “album”, “track”, “playlist”, “directory”. Read-only.
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(RefTypeConverter))]
        public RefType Type { get; set; }

        /// <summary>
        /// The object URI. Read-only.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
