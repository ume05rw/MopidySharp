using Mopidy.Models.EventArgs.Bases;
using Newtonsoft.Json;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for StreamTitleChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.stream_title_changed
    /// </remarks>
    public class StreamTitleChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "stream_title_changed";

        /// <summary>
        /// the new stream title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
