using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class MuteChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "mute_changed";

        [JsonProperty("mute")]
        public bool Mute { get; set; }
    }
}
