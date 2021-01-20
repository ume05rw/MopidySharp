using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcResultError : JsonRpcResult
    {
        [JsonProperty("error")]
        public object Error;

        public JsonRpcResultError(int id, object error) : base(id)
        {
            this.Error = error;
        }
    }
}
