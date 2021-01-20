using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcQueryRequest : JsonRpcQuery
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("method")]
        public string Method;

        public JsonRpcQueryRequest(int id, string method)
        {
            this.Id = id;
            this.Method = method;
        }
    }
}
