using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class JsonRpcQueryNoticeWithParams : JsonRpcQueryNotice
    {
        [JsonProperty("params")]
        public object Params;

        public JsonRpcQueryNoticeWithParams(string method, object @params) : base(method)
        {
            this.Params = @params;
        }
    }
}
