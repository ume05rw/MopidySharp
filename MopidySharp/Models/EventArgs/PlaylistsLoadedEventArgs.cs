using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    public class PlaylistsLoadedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlists_loaded";
    }
}
