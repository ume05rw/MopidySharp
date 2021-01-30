using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Playback controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#playback-controller
    /// </remarks>
    public static class Playback
    {
        // Playback control
        private const string MethodPlay = "core.playback.play";
        private const string MethodNext = "core.playback.next";
        private const string MethodPrevious = "core.playback.previous";
        private const string MethodStop = "core.playback.stop";
        private const string MethodPause = "core.playback.pause";
        private const string MethodResume = "core.playback.resume";
        private const string MethodSeek = "core.playback.seek";

        // Current track
        private const string MethodGetCurrentTlTrack = "core.playback.get_current_tl_track";
        private const string MethodGetCurrentTrack = "core.playback.get_current_track";
        private const string MethodGetStreamTitle = "core.playback.get_stream_title";
        private const string MethodGetTimePosition = "core.playback.get_time_position";

        // Playback states
        private const string MethodGetState = "core.playback.get_state";
        private const string MethodSetState = "core.playback.set_state";

        // PlaybackState String
        private const string PlaybackStateStopped = "stopped";
        private const string PlaybackStatePlaying = "playing";
        private const string PlaybackStatePaused = "paused";

        /// <summary>
        /// PlaybackState
        /// </summary>
        /// <remarks>
        /// Result of GetState, Argument of SetState
        /// </remarks>
        public enum PlaybackState
        {
            /// <summary>
            /// Stopped
            /// </summary>
            Stopped,

            /// <summary>
            /// Playing
            /// </summary>
            Playing,

            /// <summary>
            /// Paused
            /// </summary>
            Paused
        }

        private static readonly HttpQuery _query = HttpQuery.Get();

        #region "Playback control"

        /// <summary>
        /// Play the given track, or if the given tl_track and tlid is None,
        /// play the currently active track.
        /// </summary>
        /// <param name="tlId">TLID of the track to play</param>
        /// <returns></returns>
        /// <remarks>
        /// Note that the track must already be in the tracklist.
        ///
        /// Need to wait a little bit until get the correct return value of GetState.
        /// </remarks>
        public static async Task<bool> Play(
            int? tlId = null
        )
        {
            var notice = JsonRpcFactory.CreateNotice(
                Playback.MethodPlay,
                new
                {
                    tlid = tlId
                });

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Change to the next track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The current playback state will be kept.
        /// If it was playing, playing will continue.
        /// If it was paused, it will still be paused, etc.
        /// </remarks>
        public static async Task<bool> Next()
        {
            var notice = JsonRpcFactory.CreateNotice(Playback.MethodNext);

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Change to the previous track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The current playback state will be kept.
        /// If it was playing, playing will continue.
        /// If it was paused, it will still be paused, etc.
        /// </remarks>
        public static async Task<bool> Previous()
        {
            var notice = JsonRpcFactory.CreateNotice(Playback.MethodPrevious);

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Stop playing.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Need to wait a little bit until get the correct return value of GetState.
        /// </remarks>
        public static async Task<bool> Stop()
        {
            var notice = JsonRpcFactory.CreateNotice(Playback.MethodStop);

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Pause playback.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Need to wait a little bit until get the correct return value of GetState.
        /// </remarks>
        public static async Task<bool> Pause()
        {
            var notice = JsonRpcFactory.CreateNotice(Playback.MethodPause);

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// If paused, resume playing the current track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Need to wait a little bit until get the correct return value of GetState.
        /// </remarks>
        public static async Task<bool> Resume()
        {
            var notice = JsonRpcFactory.CreateNotice(Playback.MethodResume);

            var response = await Playback._query.Exec(notice);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Seeks to time position given in milliseconds.
        /// </summary>
        /// <param name="timePosition">time position in milliseconds</param>
        /// <returns></returns>
        /// <remarks>
        /// Need to wait a little while until get the correct return value for GetTimePosition.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> Seek(int timePosition)
        {
            var request = JsonRpcFactory.CreateRequest(
                Playback.MethodSeek,
                new
                {
                    time_position = timePosition
                }
            );

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, false);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<bool>();

            return (true, result);
        }

        #endregion "Playback control"

        #region "Current track"

        /// <summary>
        /// Get the currently playing or selected track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// After the Play, Pause, Stop and Resume methods,
        /// need to wait a bit for the value to change.
        /// </remarks>
        public static async Task<(bool Succeeded, TlTrack Result)> GetCurrentTlTrack()
        {
            var request = JsonRpcFactory.CreateRequest(Playback.MethodGetCurrentTlTrack);

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JObject.FromObject(response.Result).ToObject<TlTrack>();

            return (true, result);
        }

        /// <summary>
        /// Get the currently playing or selected track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Extracted from get_current_tl_track() for convenience.
        ///
        /// After the Play, Pause, Stop and Resume methods,
        /// need to wait a bit for the value to change.
        /// </remarks>
        public static async Task<(bool Succeeded, Track Result)> GetCurrentTrack()
        {
            var request = JsonRpcFactory.CreateRequest(Playback.MethodGetCurrentTrack);

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JObject.FromObject(response.Result).ToObject<Track>();

            return (true, result);
        }

        /// <summary>
        /// Get the current stream title or None. ** always null? **
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, string Result)> GetStreamTitle()
        {
            var request = JsonRpcFactory.CreateRequest(Playback.MethodGetStreamTitle);

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JValue.FromObject(response.Result).ToObject<string>();

            return (true, result);
        }

        /// <summary>
        /// Get time position in milliseconds.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// After the Seek methods,
        /// need to wait a bit for the value to change.
        /// </remarks>
        public static async Task<(bool Succeeded, int Result)> GetTimePosition()
        {
            var request = JsonRpcFactory.CreateRequest(Playback.MethodGetTimePosition);

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, -1);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<int>();

            return (true, result);
        }

        #endregion "Current track"

        #region "Playback states"

        /// <summary>
        /// Get The playback state.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// After the Play, Pause, Stop and Resume methods,
        /// need to wait a bit for the state to change.
        /// </remarks>
        public static async Task<(bool Succeeded, PlaybackState Result)> GetState()
        {
            var request = JsonRpcFactory.CreateRequest(Playback.MethodGetState);

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, PlaybackState.Stopped);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。

            var stateString = JValue.FromObject(response.Result).ToObject<string>();
            var result = Playback.GetPlaybackState(stateString);

            return (true, result);
        }

        /// <summary>
        /// Set the playback state.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static async Task<bool> SetState(PlaybackState state)
        {
            var stateString = Playback.GetPlaybackStateString(state);

            var request = JsonRpcFactory.CreateRequest(
                Playback.MethodSetState,
                new
                {
                    new_state = stateString
                }
            );

            var response = await Playback._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        private static PlaybackState GetPlaybackState(string stateString)
        {
            switch (stateString)
            {
                case Playback.PlaybackStateStopped: return PlaybackState.Stopped;
                case Playback.PlaybackStatePlaying: return PlaybackState.Playing;
                case Playback.PlaybackStatePaused: return PlaybackState.Paused;
                default:
                    throw new FormatException($"Unexpected PlaybackStateString<{stateString}>");
            }
        }

        private static string GetPlaybackStateString(PlaybackState state)
        {
            switch (state)
            {
                case PlaybackState.Stopped: return Playback.PlaybackStateStopped;
                case PlaybackState.Playing: return Playback.PlaybackStatePlaying;
                case PlaybackState.Paused: return Playback.PlaybackStatePaused;
                default:
                    throw new ArgumentException($"Unexpected PlaybackState<{state}>");
            }
        }

        #endregion "Playback states"
    }
}
