using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcResultSucceeded : JsonRpcResult
    {
        [JsonProperty("result")]
        public object Result;

        public JsonRpcResultSucceeded(int id, object result) : base(id)
        {
            this.Result = result;
        }
    }
}
