using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Playlists controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#playlists-controller
    /// </remarks>
    public static class Playlists
    {
        private const string MethodGetUriSchemes = "core.playlists.get_uri_schemes";

        // Fetching
        private const string MethodAsList = "core.playlists.as_list";
        private const string MethodGetItems = "core.playlists.get_items";
        private const string MethodLookup = "core.playlists.lookup";
        private const string MethodRefresh = "core.playlists.refresh";

        // Manipulating
        private const string MethodCreate = "core.playlists.create";
        private const string MethodSave = "core.playlists.save";
        private const string MethodDelete = "core.playlists.delete";

        private class PlaylistArg
        {
            [JsonProperty("__model__", NullValueHandling = NullValueHandling.Ignore)]
            public string ModelType { get; } = "Playlist";

            [JsonProperty("tracks", NullValueHandling = NullValueHandling.Ignore)]
            public List<TrackArg> Tracks { get; } = new List<TrackArg>();

            [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
            public string Uri { get; set; }

            [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }
        }

        private class TrackArg
        {
            [JsonProperty("__model__", NullValueHandling = NullValueHandling.Ignore)]
            public string ModelType { get; } = "Track";

            [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
            public string Uri { get; set; }
        }

        private static readonly Query _query = Query.Get();


        /// <summary>
        /// Get the list of URI schemes that support playlists.
        /// </summary>
        /// <returns></returns>
        public static async Task<(bool Succeeded, string[] Result)> GetUriSchemes()
        {
            var request = JsonRpcFactory.CreateRequest(Playlists.MethodGetUriSchemes);

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<string[]>();

            return (true, result);
        }

        #region "Fetching"

        /// <summary>
        /// Get a list of the currently available playlists.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Returns a list of Ref objects referring to the playlists.
        /// In other words, no information about the playlists’ content is given.
        /// </remarks>
        public static async Task<(bool Succeeded, Ref[] Result)> AsList()
        {
            var request = JsonRpcFactory.CreateRequest(Playlists.MethodAsList);

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<Ref[]>();

            return (true, result);
        }

        /// <summary>
        /// Get the items in a playlist specified by uri.
        /// </summary>
        /// <param name="uri">playlist URI</param>
        /// <returns></returns>
        /// <remarks>
        /// Returns a list of Ref objects referring to the playlist’s items.
        /// If a playlist with the given uri doesn’t exist, it returns None.
        /// </remarks>
        public static async Task<(bool Succeeded, Ref[] Result)> GetItems(
            string uri
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodGetItems,
                new
                {
                    uri
                }
            );

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JArray.FromObject(response.Result).ToObject<Ref[]>();

            return (true, result);
        }

        /// <summary>
        /// Lookup playlist with given URI in both the set of playlists
        /// and in any other playlist sources.
        /// </summary>
        /// <param name="uri">playlist URI</param>
        /// <returns></returns>
        /// <remarks>
        /// Returns None if not found.
        /// </remarks>
        public static async Task<(bool Succeeded, Playlist Result)> Lookup(
            string uri
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodLookup,
                new
                {
                    uri
                }
            );

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JObject.FromObject(response.Result).ToObject<Playlist>();

            return (true, result);
        }

        /// <summary>
        /// Refresh the playlists in playlists.
        /// </summary>
        /// <param name="uriScheme">limit to the backend matching the URI scheme</param>
        /// <returns></returns>
        /// <remarks>
        /// If uri_scheme is None, all backends are asked to refresh.
        /// If uri_scheme is an URI scheme handled by a backend, only that
        /// backend is asked to refresh.
        /// If uri_scheme doesn’t match any current backend, nothing happens.
        /// </remarks>
        public static async Task<bool> Refresh(
            string uriScheme = null
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodRefresh,
                new
                {
                    uri_scheme = uriScheme
                }
            );

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        #endregion "Fetching"

        #region "Manipulating"

        /// <summary>
        /// Create a new playlist.
        /// </summary>
        /// <param name="name">name of the new playlist</param>
        /// <param name="uriScheme">use the backend matching the URI scheme</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        public static async Task<(bool Succeeded, Playlist Result)> Create(
            string name,
            string uriScheme = null
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodCreate,
                new
                {
                    name,
                    uri_scheme = uriScheme
                }
            );

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JObject.FromObject(response.Result).ToObject<Playlist>();

            return (true, result);
        }

        /// <summary>
        /// Save the playlist.
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <remarks>
        /// For a playlist to be saveable, it must have the uri attribute set.
        /// You must not set the uri atribute yourself, but use playlist objects
        /// returned by create() or retrieved from playlists, which will always give you
        /// saveable playlists.
        ///
        /// The method returns the saved playlist.
        /// The return playlist may differ from the saved playlist.
        /// E.g. if the playlist name was changed, the returned playlist may have
        /// a different URI.
        /// The caller of this method must throw away the playlist sent to this method,
        /// and use the returned playlist instead.
        ///
        /// If the playlist’s URI isn’t set or doesn’t match the URI scheme
        /// of a current backend, nothing is done and None is returned.
        /// </remarks>
        public static async Task<(bool Succeeded, Playlist Result)> Save(
            Playlist playlist
        )
        {
            if (playlist == null)
                return (false, null);

            var args = new PlaylistArg()
            {
                Uri = playlist.Uri,
                Name = playlist.Name
            };

            if (playlist.Tracks != null && 0 <= playlist.Tracks.Count)
            {
                foreach (var track in playlist.Tracks)
                {
                    args.Tracks.Add(new TrackArg()
                    {
                        Uri = track.Uri
                    });
                }
            }

            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodSave,
                new
                {
                    playlist = args
                }
            );

            var response = await Playlists._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = (response.Result == null)
                ? null
                : JObject.FromObject(response.Result).ToObject<Playlist>();

            return (true, result);
        }

        /// <summary>
        /// Delete playlist identified by the URI.
        /// </summary>
        /// <param name="uri">URI of the playlist to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// If the URI doesn’t match the URI schemes handled by the current backends,
        /// nothing happens.
        ///
        /// Returns True if deleted, False otherwise.
        /// </remarks>
        public static async Task<(bool Succeeded, bool Result)> Delete(
            string uri
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Playlists.MethodDelete,
                new
                {
                    uri
                }
            );

            var response = await Playlists._query.Exec(request);

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

        #endregion "Manipulating"
    }
}
