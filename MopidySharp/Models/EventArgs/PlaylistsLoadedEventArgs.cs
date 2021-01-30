using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for PlaylistsLoaded
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlists_loaded
    /// </remarks>
    public class PlaylistsLoadedEventArgs : EventArgsBase
    {
        internal const string EventName = "playlists_loaded";
    }
}
