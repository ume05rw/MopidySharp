using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcParamsResponse : JsonRpcBase
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }
}
