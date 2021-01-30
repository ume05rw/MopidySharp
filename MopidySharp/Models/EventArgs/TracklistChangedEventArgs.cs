using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for TracklistChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.tracklist_changed
    /// </remarks>
    public class TracklistChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "tracklist_changed";
    }
}
