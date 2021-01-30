using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mopidy.Models.Enums
{
    internal class RefTypeConverter : JsonConverter
    {
        // Type String
        private const string RefTypeAlbum = "album";
        private const string RefTypeArtist = "artist";
        private const string RefTypeDirectory = "directory";
        private const string RefTypePlaylist = "playlist";
        private const string RefTypeTrack = "track";

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

            switch (type)
            {
                case RefType.Album:
                    writer.WriteValue(RefTypeConverter.RefTypeAlbum);
                    break;
                case RefType.Artist:
                    writer.WriteValue(RefTypeConverter.RefTypeArtist);
                    break;
                case RefType.Directory:
                    writer.WriteValue(RefTypeConverter.RefTypeDirectory);
                    break;
                case RefType.Playlist:
                    writer.WriteValue(RefTypeConverter.RefTypePlaylist);
                    break;
                case RefType.Track:
                    writer.WriteValue(RefTypeConverter.RefTypeTrack);
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
                case RefTypeConverter.RefTypeAlbum:
                    return RefType.Album;
                case RefTypeConverter.RefTypeArtist:
                    return RefType.Artist;
                case RefTypeConverter.RefTypeDirectory:
                    return RefType.Directory;
                case RefTypeConverter.RefTypePlaylist:
                    return RefType.Playlist;
                case RefTypeConverter.RefTypeTrack:
                    return RefType.Track;
                default:
                    throw new JsonReaderException($"Unexpected Value[type]<{typeString}>");
            }
        }
    }

    /// <summary>
    /// Type of Ref
    /// </summary>
    [JsonConverter(typeof(RefTypeConverter))]
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
}
