using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal abstract class JsonRpcBase
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; } = "2.0";
    }
}
