using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class SeekedEventArgs : EventArgsBase
    {
        internal const string EventName = "seeked";

        [JsonProperty("time_position")]
        public int TimePosition { get; set; }
    }
}
