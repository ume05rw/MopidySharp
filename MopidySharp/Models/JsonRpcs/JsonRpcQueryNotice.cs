using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcQueryNotice : JsonRpcQuery
    {
        [JsonProperty("method")]
        public string Method;

        public JsonRpcQueryNotice(string method) : base()
        {
            this.Method = method;
        }
    }
}
