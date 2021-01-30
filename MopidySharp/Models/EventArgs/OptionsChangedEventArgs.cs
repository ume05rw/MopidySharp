using Mopidy.Models.EventArgs.Bases;

namespace Mopidy.Models.EventArgs
{
    public class OptionsChangedEventArgs : EventArgsBase
    {
        internal const string EventName = "options_changed";
    }
}
