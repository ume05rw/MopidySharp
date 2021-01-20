using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
     [JsonObject(MemberSerialization.OptIn)]
    internal abstract class JsonRpcResult : JsonRpcBase
    {
        [JsonProperty("id")]
        public int Id;

        protected JsonRpcResult(int id)
        {
            this.Id = id;
        }
    }
}
