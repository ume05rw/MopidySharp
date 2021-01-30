using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    public class TracklistChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "tracklist_changed";
    }
}
