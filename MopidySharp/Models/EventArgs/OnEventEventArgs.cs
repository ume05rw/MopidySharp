//using Mopidy.Models.EventArgs.Bases;
//using Newtonsoft.Json;

//namespace Mopidy.Models.EventArgs
//{
//    /// <summary>
//    /// EventArgs for OnEvent ** Can't receive "on_event" ? **
//    /// </summary>
//    /// <remarks>
//    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.on_event
//    /// </remarks>
//    public class OnEventEventArgs : EventArgsBase
//    {
//        internal const string EventName = "on_event";
//
//        /// <summary>
//        /// any other arguments to the specific event handlers
//        /// </summary>
//        [JsonProperty("kwargs")]
//        public dynamic KwArgs { get; set; }
//    }
//}
