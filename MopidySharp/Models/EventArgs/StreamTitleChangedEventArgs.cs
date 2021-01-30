using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    public class StreamTitleChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "stream_title_changed";

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
