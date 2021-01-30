using Mopidy.Models.EventArgs.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Mopidy.Models.JsonRpcs.Interfaces
{
    internal class RecievedConverter : JsonConverter
    {
        private const string RecievedJsonRpc = "jsonrpc";
        private const string RecievedEvent = "event";

        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
            => (objectType == typeof(IRecieved));

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
            if (jObject.TryGetValue(RecievedConverter.RecievedJsonRpc, out _))
            {
                var result = new JsonRpcParamsResponse();
                serializer.Populate(jObject.CreateReader(), result);

                return result;
            }
            if (jObject.TryGetValue(RecievedConverter.RecievedEvent, out _))
            {
                return jObject.ToObject<IEventArgs>();
            }

            return null;
        }
    }

    [JsonConverter(typeof(RecievedConverter))]
    public interface IRecieved
    {
    }
}
