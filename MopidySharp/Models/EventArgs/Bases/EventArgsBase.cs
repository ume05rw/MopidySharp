using Mopidy.Models.EventArgs.Interfaces;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs.Bases
{
    /// <summary>
    /// Abstract EventArgs
    /// </summary>
    public abstract class EventArgsBase : System.EventArgs, IEventArgs
    {
        /// <summary>
        /// Event Name
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
