using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class VolumeChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "volume_changed";

        [JsonProperty("volume")]
        public int Volume { get; set; }
    }
}
