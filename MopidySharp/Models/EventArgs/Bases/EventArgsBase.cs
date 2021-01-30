using Mopidy.Models.EventArgs.Interfaces;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs.Bases
{
    public abstract class EventArgsBase : System.EventArgs, IEventArgs
    {
        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
