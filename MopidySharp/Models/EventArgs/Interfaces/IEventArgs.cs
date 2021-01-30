using Mopidy.Models.JsonRpcs.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Mopidy.Models.EventArgs.Interfaces
{
    internal class EventArgsConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
            => (objectType == typeof(IEventArgs));

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer
        )
        {
            var jObject = JObject.Load(reader);
            var eventName = jObject["event"].Value<string>();
            var jReader = jObject.CreateReader();

            switch (eventName)
            {
                case EventArgs.MuteChangedEventArgs.EventName:
                    {
                        var args = new MuteChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }

                // ** Can't receive "on_event" ? **
                //case EventArgs.OnEventEventArgs.EventName:
                //    {
                //        var args = new OnEventEventArgs();
                //        serializer.Populate(jReader, args);

                //        return args;
                //    }

                case EventArgs.OptionsChangedEventArgs.EventName:
                    {
                        var args = new OptionsChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.PlaybackStateChangedEventArgs.EventName:
                    {
                        var args = new PlaybackStateChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.PlaylistChangedEventArgs.EventName:
                    {
                        var args = new PlaylistChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.PlaylistDeletedEventArgs.EventName:
                    {
                        var args = new PlaylistDeletedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.PlaylistsLoadedEventArgs.EventName:
                    {
                        var args = new PlaylistsLoadedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.SeekedEventArgs.EventName:
                    {
                        var args = new SeekedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.StreamTitleChangedEventArgs.EventName:
                    {
                        var args = new StreamTitleChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.TracklistChangedEventArgs.EventName:
                    {
                        var args = new TracklistChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.TrackPlaybackEndedEventArgs.EventName:
                    {
                        var args = new TrackPlaybackEndedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.TrackPlaybackPausedEventArgs.EventName:
                    {
                        var args = new TrackPlaybackPausedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.TrackPlaybackResumedEventArgs.EventName:
                    {
                        var args = new TrackPlaybackResumedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.TrackPlaybackStartedEventArgs.EventName:
                    {
                        var args = new TrackPlaybackStartedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                case EventArgs.VolumeChangedEventArgs.EventName:
                    {
                        var args = new VolumeChangedEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
                default:
                    {
                        var args = new UnexpectedEventEventArgs();
                        serializer.Populate(jReader, args);

                        return args;
                    }
            }
        }
    }

    /// <summary>
    /// EventArgs Interface
    /// </summary>
    [JsonConverter(typeof(EventArgsConverter))]
    public interface IEventArgs : IRecieved
    {
        /// <summary>
        /// Event Name
        /// </summary>
        string Event { get; set; }
    }
}
