using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Tracklist controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#tracklist-controller
    /// </remarks>
    public static class Tracklist
    {
        // Manipulating
        private const string MethodAdd = "core.tracklist.add";
        private const string MethodRemove = "core.tracklist.remove";
        private const string MethodClear = "core.tracklist.clear";
        private const string MethodMove = "core.tracklist.move";
        private const string MethodShuffle = "core.tracklist.shuffle";

        // Current state
        private const string MethodGetTlTracks = "core.tracklist.get_tl_tracks";
        private const string MethodIndex = "core.tracklist.index";
        private const string MethodGetVersion = "core.tracklist.get_version";
        private const string MethodGetLength = "core.tracklist.get_length";
        private const string MethodGetTracks = "core.tracklist.get_tracks";
        private const string MethodSlice = "core.tracklist.slice";
        private const string MethodFilter = "core.tracklist.filter";

        // Future state
        private const string MethodGetEotTlId = "core.tracklist.get_eot_tlid";
        private const string MethodGetNextTlId = "core.tracklist.get_next_tlid";
        private const string MethodGetPreviousTlId = "core.tracklist.get_previous_tlid";
        // Deprecated
        //private const string MethodEotTrack = "core.tracklist.eot_track";
        //private const string MethodNextTrack = "core.tracklist.next_track";
        //private const string MethodPreviousTrack = "core.tracklist.previous_track";

        // Options
        private const string MethodGetConsume = "core.tracklist.get_consume";
        private const string MethodSetConsume = "core.tracklist.set_consume";
        private const string MethodGetRandom = "core.tracklist.get_random";
        private const string MethodSetRandom = "core.tracklist.set_random";
        private const string MethodGetRepeat = "core.tracklist.get_repeat";
        private const string MethodSetRepeat = "core.tracklist.set_repeat";
        private const string MethodGetSingle = "core.tracklist.get_single";
        private const string MethodSetSingle = "core.tracklist.set_single";

        /// <summary>
        /// Argument of Remove, Filter Method
        /// </summary>
        public class Criteria
        {
            /// <summary>
            /// Tracklist ID number list
            /// </summary>
            [JsonProperty("tlid", NullValueHandling = NullValueHandling.Ignore)]
            public List<int> TlId { get; set; } = new List<int>();

            /// <summary>
            /// URI string list
            /// </summary>
            [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> Uri { get; set; } = new List<string>();

            /// <summary>
            /// Clear and Initialize
            /// </summary>
            public void Clear()
            {
                if (this.TlId == null)
                    this.TlId = new List<int>();
                else
                    this.TlId.Clear();

                if (this.Uri == null)
                    this.Uri = new List<string>();
                else
                    this.Uri.Clear();
            }

            internal Criteria Format()
            {
                var result = new Criteria()
                {
                    TlId = (this.TlId != null && 0 < this.TlId.Count)
                        ? this.TlId
                        : null,
                    Uri = (this.Uri != null && 0 < this.Uri.Count)
                        ? this.Uri
                        : null
                };

                return result;
            }
        }


        private static readonly QueryHttp _query = QueryHttp.Get();

        #region "Manipulating"

        /// <summary>
        /// Add tracks to the tracklist.
        /// </summary>
        /// <param name="uris">list of URIs for tracks to add</param>
        /// <param name="atPosition">position in tracklist to add tracks</param>
        /// <returns></returns>
        /// <remarks>
        /// If uris is given instead of tracks, the URIs are looked up in the library
        /// and the resulting tracks are added to the tracklist.
        /// If at_position is given, the tracks are inserted at the given position
        /// in the tracklist.If at_position is not given,
        /// the tracks are appended to the end of the tracklist.
        /// </remarks>
        public static async Task<(bool Succeeded, TlTrack[] Result)> Add(
            string[] uris,
            int? atPosition = null
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodAdd,
                new
                {
                    at_position = atPosition,
                    uris
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<TlTrack[]>();

            return (true, result);
        }

        /// <summary>
        /// Add tracks to the tracklist.
        /// </summary>
        /// <param name="uri">URI for track to add</param>
        /// <param name="atPosition">position in tracklist to add track</param>
        /// <returns></returns>
        /// <remarks>
        /// If uris is given instead of tracks, the URIs are looked up in the library
        /// and the resulting tracks are added to the tracklist.
        /// If at_position is given, the tracks are inserted at the given position
        /// in the tracklist.If at_position is not given,
        /// the tracks are appended to the end of the tracklist.
        /// </remarks>
        public static Task<(bool Succeeded, TlTrack[] Result)> Add(
            string uri,
            int? atPosition = null
        )
        {
            return Tracklist.Add(new string[] { uri }, atPosition);
        }

        /// <summary>
        /// Remove the matching tracks from the tracklist.
        /// </summary>
        /// <param name="criteria">one or more rules to match by</param>
        /// <returns></returns>
        private static async Task<(bool Succeeded, TlTrack[] Result)> Remove(
            Criteria criteria
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodRemove,
                criteria?.Format()
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<TlTrack[]>();

            return (true, result);
        }

        /// <summary>
        /// Remove the matching tracks from the tracklist.
        /// </summary>
        /// <param name="tlId">one or more rules to match by</param>
        /// <returns></returns>
        public static Task<(bool Succeeded, TlTrack[] Result)> Remove(
            int[] tlId
        )
        {
            var criteria = new Criteria();
            if (tlId != null && 0 < tlId.Length)
                criteria.TlId.AddRange(tlId);

            return Tracklist.Remove(criteria);
        }

        /// <summary>
        /// Remove the matching tracks from the tracklist.
        /// </summary>
        /// <param name="uri">one or more rules to match by</param>
        /// <returns></returns>
        public static Task<(bool Succeeded, TlTrack[] Result)> Remove(
            string[] uri
        )
        {
            var criteria = new Criteria();
            if (uri != null && 0 < uri.Length)
                criteria.Uri.AddRange(uri);

            return Tracklist.Remove(criteria);
        }

        /// <summary>
        /// Remove the matching tracks from the tracklist.
        /// </summary>
        /// <param name="tlId">one or more rules to match by</param>
        /// <returns></returns>
        public static Task<(bool Succeeded, TlTrack[] Result)> Remove(
            int tlId
        )
        {
            var criteria = new Criteria();
            criteria.TlId.Add((int)tlId);

            return Tracklist.Remove(criteria);
        }

        /// <summary>
        /// Remove the matching tracks from the tracklist.
        /// </summary>
        /// <param name="uri">one or more rules to match by</param>
        /// <returns></returns>
        public static Task<(bool Succeeded, TlTrack[] Result)> Remove(
            string uri
        )
        {
            var criteria = new Criteria();
            if (uri != null)
                criteria.Uri.Add(uri);

            return Tracklist.Remove(criteria);
        }

        /// <summary>
        /// Clear the tracklist.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> Clear()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodClear);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Move the tracks in the slice [start:end] to to_position.
        /// </summary>
        /// <param name="start">position of first track to move</param>
        /// <param name="end">position after last track to move ** end track is not target! **</param>
        /// <param name="toPosition">new position for the tracks</param>
        /// <returns></returns>
        public static async Task<bool> Move(int start, int end, int toPosition)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodMove,
                new
                {
                    start,
                    end,
                    to_position = toPosition
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shuffles the entire tracklist.
        /// If start and end is given only shuffles the slice [start:end].
        /// </summary>
        /// <param name="start">position of first track to shuffle</param>
        /// <param name="end">position after last track to shuffle ** end track is not target! **</param>
        /// <returns></returns>
        public static async Task<bool> Shuffle(int? start = null, int? end = null)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodShuffle,
                new
                {
                    start,
                    end
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        #endregion "Manipulating"

        #region "Current state"

        /// <summary>
        /// Get tracklist as list of mopidy.models.TlTrack.
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, TlTrack[] Result)> GetTlTracks()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetTlTracks);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<TlTrack[]>();

            return (true, result);
        }

        /// <summary>
        /// The position of the given track in the tracklist.
        /// </summary>
        /// <param name="tlId">TLID of the track to find the index of</param>
        /// <returns></returns>
        /// <remarks>
        /// ** Argument tl_track is not works **
        ///
        /// If neither tl_track or tlid is given we return the index of
        /// the currently playing track.
        /// </remarks>
        public static async Task<(bool Succeeded, int? Result)> Index(
            int tlId
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodIndex,
                new
                {
                    tlid = tlId
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JValue.FromObject(response.Result).ToObject<int?>();

            return (true, result);
        }

        /// <summary>
        /// Get the tracklist version.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Integer which is increased every time the tracklist is changed.
        /// Is not reset before Mopidy is restarted.
        /// </remarks>
        public static async Task<(bool Succeeded, int Result)> GetVersion()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetVersion);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Get length of the tracklist.
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, int Result)> GetLength()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetLength);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Get tracklist as list of mopidy.models.Track.
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, Track[] Result)> GetTracks()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetTracks);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<Track[]>();

            return (true, result);
        }

        /// <summary>
        /// Returns a slice of the tracklist, limited by the given start and end positions.
        /// </summary>
        /// <param name="start">position of first track to include in slice</param>
        /// <param name="end">position after last track to include in slice ** end track is not target! **</param>
        /// <returns></returns>
        public static async Task<(bool Succeeded, TlTrack[] Result)> Slice(
            int start,
            int end
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodSlice,
                new
                {
                    start,
                    end
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<TlTrack[]>();

            return (true, result);
        }

        /// <summary>
        /// Filter the tracklist by the given criteria.
        /// </summary>
        /// <param name="criteria">one or more rules to match by</param>
        /// <returns></returns>
        /// <remarks>
        /// Each rule in the criteria consists of a model field
        /// and a list of values to compare it against.
        /// If the model field matches any of the values, it may be returned.
        /// Only tracks that match all the given criteria are returned.
        /// </remarks>
        public static async Task<(bool Succeeded, TlTrack[] Result)> Filter(
            Criteria criteria
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodFilter,
                criteria?.Format()
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<TlTrack[]>();

            return (true, result);
        }

        /// <summary>
        /// Filter the tracklist by the given criteria.
        /// </summary>
        /// <param name="tlId">one or more rules to match by</param>
        /// <param name="uri">one or more rules to match by</param>
        /// <returns></returns>
        /// <remarks>
        /// Each rule in the criteria consists of a model field
        /// and a list of values to compare it against.
        /// If the model field matches any of the values, it may be returned.
        /// Only tracks that match all the given criteria are returned.
        /// </remarks>
        public static Task<(bool Succeeded, TlTrack[] Result)> Filter(
            int[] tlId = null,
            string[] uri = null
        )
        {
            var criteria = new Criteria();
            if (tlId != null && 0 < tlId.Length)
                criteria.TlId.AddRange(tlId);
            if (uri != null && 0 < uri.Length)
                criteria.Uri.AddRange(uri);

            return Tracklist.Filter(criteria);
        }

        /// <summary>
        /// Filter the tracklist by the given criteria.
        /// </summary>
        /// <param name="tlId">one or more rules to match by</param>
        /// <param name="uri">one or more rules to match by</param>
        /// <returns></returns>
        /// <remarks>
        /// Each rule in the criteria consists of a model field
        /// and a list of values to compare it against.
        /// If the model field matches any of the values, it may be returned.
        /// Only tracks that match all the given criteria are returned.
        /// </remarks>
        public static Task<(bool Succeeded, TlTrack[] Result)> Filter(
            int? tlId = null,
            string uri = null
        )
        {
            var criteria = new Criteria();

            if (tlId != null)
                criteria.TlId.Add((int)tlId);
            if (uri != null)
                criteria.Uri.Add(uri);

            return Tracklist.Filter(criteria);
        }

        #endregion "Current state"

        #region "Future state"

        /// <summary>
        /// The TLID of the track that will be played after the current track.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Not necessarily the same TLID as returned by get_next_tlid().
        /// </remarks>
        public static async Task<(bool Succeeded, int? Result)> GetEotTlId()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetEotTlId);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JValue.FromObject(response.Result).ToObject<int?>();

            return (true, result);
        }

        /// <summary>
        /// The tlid of the track that will be played if calling
        /// mopidy.core.PlaybackController.next().
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// For normal playback this is the next track in the tracklist.
        /// If repeat is enabled the next track can loop around the tracklist.
        /// When random is enabled this should be a random track,
        /// all tracks should be played once before the tracklist repeats.
        /// </remarks>
        public static async Task<(bool Succeeded, int? Result)> GetNextTlId()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetNextTlId);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JValue.FromObject(response.Result).ToObject<int?>();

            return (true, result);
        }

        /// <summary>
        /// Returns the TLID of the track that will be played if calling
        /// mopidy.core.PlaybackController.previous().
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// For normal playback this is the previous track in the tracklist.
        /// If random and/or consume is enabled it should return the current track instead.
        /// </remarks>
        public static async Task<(bool Succeeded, int? Result)> GetPreviousTlId()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetPreviousTlId);

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JValue.FromObject(response.Result).ToObject<int?>();

            return (true, result);
        }

        #endregion "Future state"

        #region "Options"

        /// <summary>
        /// Get consume mode.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// true : Tracks are removed from the tracklist when they have been played.
        /// false: Tracks are not removed from the tracklist.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> GetConsume()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetConsume);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Set consume mode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// true : Tracks are removed from the tracklist when they have been played.
        /// false: Tracks are not removed from the tracklist.
        /// </remarks>
        public static async Task<bool> SetConsume(bool value)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodSetConsume,
                new
                {
                    value
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get random mode.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// true : Tracks are selected at random from the tracklist.
        /// false: Tracks are played in the order of the tracklist.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> GetRandom()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetRandom);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Set random mode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// true : Tracks are selected at random from the tracklist.
        /// false: Tracks are played in the order of the tracklist.
        /// </remarks>
        public static async Task<bool> SetRandom(bool value)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodSetRandom,
                new
                {
                    value
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get repeat mode.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// true : The tracklist is played repeatedly.
        /// false: The tracklist is played once.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> GetRepeat()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetRepeat);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Set repeat mode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// true : The tracklist is played repeatedly.
        /// false: The tracklist is played once.
        /// </remarks>
        public static async Task<bool> SetRepeat(bool value)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodSetRepeat,
                new
                {
                    value
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get single mode.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// true : Playback is stopped after current song, unless in repeat mode.
        /// false: Playback continues after current song.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> GetSingle()
        {
            var request = JsonRpcFactory.CreateRequest(Tracklist.MethodGetSingle);

            var response = await Tracklist._query.Exec(request);

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

        /// <summary>
        /// Set single mode.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// true : Playback is stopped after current song, unless in repeat mode.
        /// false: Playback continues after current song.
        /// </remarks>
        public static async Task<bool> SetSingle(bool value)
        {
            var request = JsonRpcFactory.CreateRequest(
                Tracklist.MethodSetSingle,
                new
                {
                    value
                }
            );

            var response = await Tracklist._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        #endregion "Options"
    }
}
