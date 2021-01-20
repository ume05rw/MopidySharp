using Newtonsoft.Json;

namespace Mopidy.Models.JsonRpcs
{
    [JsonObject(MemberSerialization.OptIn)]
    internal abstract class JsonRpcQuery : JsonRpcBase
    {
    }
}
