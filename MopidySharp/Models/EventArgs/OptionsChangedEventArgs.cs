using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    /// <summary>
    /// EventArgs for OptionsChanged
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.options_changed
    /// </remarks>
    public class OptionsChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "options_changed";
    }
}
