using Mopidy.Models.EventArgs;
using Mopidy.Models.EventArgs.Interfaces;
using System;

namespace Mopidy.Core
{
    /// <summary>
    /// Event Listener
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#core-events
    /// </remarks>
    public class CoreListener
    {
        /// <summary>
        /// Dummy Sender
        /// </summary>
        private static readonly CoreListener _instance = new CoreListener();

        internal static void FireEvent(IEventArgs eventArgs)
        {
            if (eventArgs is MuteChangedEventArgs)
            {
                CoreListener.MuteChanged?.Invoke(
                    CoreListener._instance,
                    (MuteChangedEventArgs)eventArgs
                );

                return;
            }

            // ** Can't receive "on_event" ? **
            //if (eventArgs is OnEventEventArgs)
            //{
            //    CoreListener.OnEvent?.Invoke(
            //        CoreListener._instance,
            //        (OnEventEventArgs)eventArgs
            //    );
            //
            //    return;
            //}

            if (eventArgs is OptionsChangedEventArgs)
            {
                CoreListener.OptionsChanged?.Invoke(
                    CoreListener._instance,
                    (OptionsChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is PlaybackStateChangedEventArgs)
            {
                CoreListener.PlaybackStateChanged?.Invoke(
                    CoreListener._instance,
                    (PlaybackStateChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is PlaylistChangedEventArgs)
            {
                CoreListener.PlaylistChanged?.Invoke(
                    CoreListener._instance,
                    (PlaylistChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is PlaylistDeletedEventArgs)
            {
                CoreListener.PlaylistDeleted?.Invoke(
                    CoreListener._instance,
                    (PlaylistDeletedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is PlaylistsLoadedEventArgs)
            {
                CoreListener.PlaylistsLoaded?.Invoke(
                    CoreListener._instance,
                    (PlaylistsLoadedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is SeekedEventArgs)
            {
                CoreListener.Seeked?.Invoke(
                    CoreListener._instance,
                    (SeekedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is StreamTitleChangedEventArgs)
            {
                CoreListener.StreamTitleChanged?.Invoke(
                    CoreListener._instance,
                    (StreamTitleChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is TracklistChangedEventArgs)
            {
                CoreListener.TracklistChanged?.Invoke(
                    CoreListener._instance,
                    (TracklistChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is TrackPlaybackEndedEventArgs)
            {
                CoreListener.TrackPlaybackEnded?.Invoke(
                    CoreListener._instance,
                    (TrackPlaybackEndedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is TrackPlaybackPausedEventArgs)
            {
                CoreListener.TrackPlaybackPaused?.Invoke(
                    CoreListener._instance,
                    (TrackPlaybackPausedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is TrackPlaybackResumedEventArgs)
            {
                CoreListener.TrackPlaybackResumed?.Invoke(
                    CoreListener._instance,
                    (TrackPlaybackResumedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is TrackPlaybackStartedEventArgs)
            {
                CoreListener.TrackPlaybackStarted?.Invoke(
                    CoreListener._instance,
                    (TrackPlaybackStartedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is VolumeChangedEventArgs)
            {
                CoreListener.VolumeChanged?.Invoke(
                    CoreListener._instance,
                    (VolumeChangedEventArgs)eventArgs
                );

                return;
            }

            if (eventArgs is UnexpectedEventEventArgs)
            {
                CoreListener.UnexpectedEvent?.Invoke(
                    CoreListener._instance,
                    (UnexpectedEventEventArgs)eventArgs
                );

                return;
            }
        }

        /// <summary>
        /// MuteChanged
        /// </summary>
        /// <remarks>
        /// Called whenever the mute state is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.mute_changed
        /// </remarks>
        public static EventHandler<MuteChangedEventArgs> MuteChanged;

        ///// <summary>
        ///// OnEvent ** Unable to receive? **
        ///// </summary>
        ///// <remarks>
        ///// Called on all events.
        ///// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.on_event
        ///// </remarks>
        //public static EventHandler<OnEventEventArgs> OnEvent;

        /// <summary>
        /// OptionsChanged
        /// </summary>
        /// <remarks>
        /// Called whenever an option is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.options_changed
        /// </remarks>
        public static EventHandler<OptionsChangedEventArgs> OptionsChanged;

        /// <summary>
        /// PlaybackStateChanged
        /// </summary>
        /// <remarks>
        /// Called whenever playback state is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playback_state_changed
        /// </remarks>
        public static EventHandler<PlaybackStateChangedEventArgs> PlaybackStateChanged;

        /// <summary>
        /// PlaylistChanged
        /// </summary>
        /// <remarks>
        /// Called whenever a playlist is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlist_changed
        /// </remarks>
        public static EventHandler<PlaylistChangedEventArgs> PlaylistChanged;

        /// <summary>
        /// PlaylistDeleted
        /// </summary>
        /// <remarks>
        /// Called whenever a playlist is deleted.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlist_deleted
        /// </remarks>
        public static EventHandler<PlaylistDeletedEventArgs> PlaylistDeleted;

        /// <summary>
        /// PlaylistsLoaded
        /// </summary>
        /// <remarks>
        /// Called when playlists are loaded or refreshed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.playlists_loaded
        /// </remarks>
        public static EventHandler<PlaylistsLoadedEventArgs> PlaylistsLoaded;

        /// <summary>
        /// Seeked
        /// </summary>
        /// <remarks>
        /// Called whenever the time position changes by an unexpected amount,
        /// e.g. at seek to a new time position.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.seeked
        /// </remarks>
        public static EventHandler<SeekedEventArgs> Seeked;

        /// <summary>
        /// StreamTitleChanged ** Not Tested. When happen? **
        /// </summary>
        /// <remarks>
        /// Called whenever the currently playing stream title changes.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.stream_title_changed
        /// </remarks>
        public static EventHandler<StreamTitleChangedEventArgs> StreamTitleChanged;

        /// <summary>
        /// TracklistChanged
        /// </summary>
        /// <remarks>
        /// Called whenever the tracklist is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.tracklist_changed
        /// </remarks>
        public static EventHandler<TracklistChangedEventArgs> TracklistChanged;

        /// <summary>
        /// TrackPlaybackEnded
        /// </summary>
        /// <remarks>
        /// Called whenever playback of a track ends.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_ended
        /// </remarks>
        public static EventHandler<TrackPlaybackEndedEventArgs> TrackPlaybackEnded;

        /// <summary>
        /// TrackPlaybackPaused
        /// </summary>
        /// <remarks>
        /// Called whenever track playback is paused.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_paused
        /// </remarks>
        public static EventHandler<TrackPlaybackPausedEventArgs> TrackPlaybackPaused;

        /// <summary>
        /// TrackPlaybackResumed
        /// </summary>
        /// <remarks>
        /// Called whenever track playback is resumed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_resumed
        /// </remarks>
        public static EventHandler<TrackPlaybackResumedEventArgs> TrackPlaybackResumed;

        /// <summary>
        /// TrackPlaybackStarted
        /// </summary>
        /// <remarks>
        /// Called whenever a new track starts playing.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.track_playback_started
        /// </remarks>
        public static EventHandler<TrackPlaybackStartedEventArgs> TrackPlaybackStarted;

        /// <summary>
        /// VolumeChanged
        /// </summary>
        /// <remarks>
        /// Called whenever the volume is changed.
        /// https://docs.mopidy.com/en/latest/api/core/#mopidy.core.CoreListener.volume_changed
        /// </remarks>
        public static EventHandler<VolumeChangedEventArgs> VolumeChanged;

        /// <summary>
        /// Unexpected Event.
        /// </summary>
        /// <remarks>
        /// Not Implemented Event.
        /// </remarks>
        public static EventHandler<UnexpectedEventEventArgs> UnexpectedEvent;


        private CoreListener()
        {
        }
    }
}
