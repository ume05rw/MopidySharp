using Newtonsoft.Json;
using System;

namespace Mopidy.Models.Enums
{
    internal class PlaybackStateConverter : JsonConverter
    {
        // PlaybackState String
        private const string PlaybackStateStopped = "stopped";
        private const string PlaybackStatePlaying = "playing";
        private const string PlaybackStatePaused = "paused";

        public override bool CanWrite => true;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
            => (objectType == typeof(PlaybackState));

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer
        )
        {
            PlaybackState state;

            try
            {
                state = (PlaybackState)value;
            }
            catch (Exception ex)
            {
                throw new JsonWriterException($"Unexpected Value[PlaybackState]<{value}>: {ex.Message}", ex);
            }

            switch (state)
            {
                case PlaybackState.Stopped:
                    writer.WriteValue(PlaybackStateConverter.PlaybackStateStopped);
                    break;
                case PlaybackState.Playing:
                    writer.WriteValue(PlaybackStateConverter.PlaybackStatePlaying);
                    break;
                case PlaybackState.Paused:
                    writer.WriteValue(PlaybackStateConverter.PlaybackStatePaused);
                    break;
                default:
                    throw new JsonWriterException($"Unexpected Value[PlaybackState]<{value}>");
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
                case PlaybackStateConverter.PlaybackStateStopped:
                    return PlaybackState.Stopped;
                case PlaybackStateConverter.PlaybackStatePlaying:
                    return PlaybackState.Playing;
                case PlaybackStateConverter.PlaybackStatePaused:
                    return PlaybackState.Paused;
                default:
                    throw new JsonReaderException($"Unexpected Value[type]<{typeString}>");
            }
        }
    }

    /// <summary>
    /// PlaybackState
    /// </summary>
    /// <remarks>
    /// Result of GetState, Argument of SetState
    /// </remarks>
    [JsonConverter(typeof(PlaybackStateConverter))]
    public enum PlaybackState
    {
        /// <summary>
        /// Stopped
        /// </summary>
        Stopped,

        /// <summary>
        /// Playing
        /// </summary>
        Playing,

        /// <summary>
        /// Paused
        /// </summary>
        Paused
    }
}
