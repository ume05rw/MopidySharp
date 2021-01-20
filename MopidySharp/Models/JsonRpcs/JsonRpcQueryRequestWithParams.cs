using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcQueryRequestWithParams : JsonRpcQueryRequest
    {
        [JsonProperty("params")]
        public object Params;

        public JsonRpcQueryRequestWithParams(int id, string method, object @params) : base(id, method)
        {
            this.Params = @params;
        }
    }
}
