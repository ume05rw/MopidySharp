using Mopidy.Models;
using Mopidy.Models.JsonRpcs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mopidy.Core
{
    /// <summary>
    /// Methods for Library controller
    /// </summary>
    /// <remarks>
    /// https://docs.mopidy.com/en/latest/api/core/#library-controller
    /// </remarks>
    public static class Library
    {
        private const string MethodBrowse = "core.library.browse";
        private const string MethodSearch = "core.library.search";
        private const string MethodLookup = "core.library.lookup";
        private const string MethodRefresh = "core.library.refresh";
        private const string MethodGetImages = "core.library.get_images";
        private const string MethodGetDistinct = "core.library.get_distinct";

        /// <summary>
        /// FieldType String
        /// </summary>
        private const string FieldTypeTrack = "track";
        private const string FieldTypeArtist = "artist";
        private const string FieldTypeAlbumArtist = "albumartist";
        private const string FieldTypeAlbum = "album";
        private const string FieldTypeComposer = "composer";
        private const string FieldTypePerformer = "performer";
        private const string FieldTypeDate = "date";
        private const string FieldTypeGenre = "genre";

        /// <summary>
        /// FieldType
        /// </summary>
        /// <remarks>
        /// Argument of GetDistinct
        /// </remarks>
        public enum FieldType
        {
            Track,
            Artist,
            AlbumArtist,
            Album,
            Composer,
            Performer,
            Date,
            Genre
        }

        /// <summary>
        /// Argument of Search, GetDistinct Method
        /// </summary>
        public class Query
        {
            [JsonProperty("uri")]
            public string[] Uri { get; set; } = null;

            [JsonProperty("track_name")]
            public string[] TrackName { get; set; } = null;

            [JsonProperty("album")]
            public string[] Album { get; set; } = null;

            [JsonProperty("artist")]
            public string[] Artist { get; set; } = null;

            [JsonProperty("albumartist")]
            public string[] AlbumArtist { get; set; } = null;

            [JsonProperty("composer")]
            public string[] Composer { get; set; } = null;

            [JsonProperty("performer")]
            public string[] Performer { get; set; } = null;

            [JsonProperty("track_no")]
            public int[] TrackNo { get; set; } = null;

            [JsonProperty("genre")]
            public string[] Genre { get; set; } = null;

            [JsonProperty("date")]
            public string[] Date { get; set; } = null;

            [JsonProperty("comment")]
            public string[] Comment { get; set; } = null;

            [JsonProperty("any")]
            public string[] Any { get; set; } = null;
        }

        private static readonly Models.JsonRpcs.Query _query = Models.JsonRpcs.Query.Get();

        /// <summary>
        /// Browse directories and tracks at the given uri.
        /// </summary>
        /// <param name="uri">URI to browse</param>
        /// <returns></returns>
        /// <remarks>
        /// uri is a string which represents some directory belonging to a backend.
        /// To get the intial root directories for backends pass None as the URI.
        /// Returns a list of mopidy.models.Ref objects for the directories and tracks
        /// at the given uri.
        /// </remarks>
        public static async Task<(bool Succeeded, Ref[] Result)> Browse(string uri)
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodBrowse,
                new
                {
                    uri = uri
                }
            );

            var response = await Library._query.Exec(request);

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
        /// Search the library for tracks where field contains values.
        /// </summary>
        /// <param name="query">one or more queries to search for</param>
        /// <param name="uris">zero or more URI roots to limit the search to</param>
        /// <param name="exact">if the search should use exact matching</param>
        /// <returns></returns>
        /// <remarks>
        /// field can be one of uri, track_name, album, artist, albumartist, composer,
        /// performer, track_no, genre, date, comment, or any.
        /// If uris is given, the search is limited to results from within the URI roots.
        /// For example passing uris=['file:'] will limit the search to the local backend.
        /// </remarks>
        public static async Task<(bool Succeeded, SearchResult[] Result)> Search(
            Query query,
            string[] uris = null,
            bool exact = false
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodSearch,
                new
                {
                    query,
                    uris,
                    exact
                }
            );

            var response = await Library._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JArray.FromObject(response.Result).ToObject<SearchResult[]>();

            return (true, result);
        }

        /// <summary>
        /// Search the library for tracks where field contains values.
        /// </summary>
        /// <param name="queryUri">one or more queries to search for</param>
        /// <param name="queryTrackName">one or more queries to search for</param>
        /// <param name="queryAlbum">one or more queries to search for</param>
        /// <param name="queryArtist">one or more queries to search for</param>
        /// <param name="queryAlbumArtist">one or more queries to search for</param>
        /// <param name="queryComposer">one or more queries to search for</param>
        /// <param name="queryPerformer">one or more queries to search for</param>
        /// <param name="queryTrackNo">one or more queries to search for</param>
        /// <param name="queryGenre">one or more queries to search for</param>
        /// <param name="queryDate">one or more queries to search for</param>
        /// <param name="queryComment">one or more queries to search for</param>
        /// <param name="queryAny">one or more queries to search for</param>
        /// <param name="uris">zero or more URI roots to limit the search to</param>
        /// <param name="exact">if the search should use exact matching</param>
        /// <returns></returns>
        /// <remarks>
        /// field can be one of uri, track_name, album, artist, albumartist, composer,
        /// performer, track_no, genre, date, comment, or any.
        /// If uris is given, the search is limited to results from within the URI roots.
        /// For example passing uris=['file:'] will limit the search to the local backend.
        /// </remarks>
        public static Task<(bool Succeeded, SearchResult[] Result)> Search(
            string[] queryUri = null,
            string[] queryTrackName = null,
            string[] queryAlbum = null,
            string[] queryArtist = null,
            string[] queryAlbumArtist = null,
            string[] queryComposer = null,
            string[] queryPerformer = null,
            int[] queryTrackNo = null,
            string[] queryGenre = null,
            string[] queryDate = null,
            string[] queryComment = null,
            string[] queryAny = null,

            string[] uris = null,
            bool exact = false
        )
        {
            var query = new Query()
            {
                Uri = queryUri,
                TrackName = queryTrackName,
                Album = queryAlbum,
                Artist = queryArtist,
                AlbumArtist = queryAlbumArtist,
                Composer = queryComposer,
                Performer = queryPerformer,
                TrackNo = queryTrackNo,
                Genre = queryGenre,
                Date = queryDate,
                Comment = queryComment,
                Any = queryAny
            };

            return Library.Search(query, uris, exact);
        }

        /// <summary>
        /// Search the library for tracks where field contains values.
        /// </summary>
        /// <param name="queryUri">one or more queries to search for</param>
        /// <param name="queryTrackName">one or more queries to search for</param>
        /// <param name="queryAlbum">one or more queries to search for</param>
        /// <param name="queryArtist">one or more queries to search for</param>
        /// <param name="queryAlbumArtist">one or more queries to search for</param>
        /// <param name="queryComposer">one or more queries to search for</param>
        /// <param name="queryPerformer">one or more queries to search for</param>
        /// <param name="queryTrackNo">one or more queries to search for</param>
        /// <param name="queryGenre">one or more queries to search for</param>
        /// <param name="queryDate">one or more queries to search for</param>
        /// <param name="queryComment">one or more queries to search for</param>
        /// <param name="queryAny">one or more queries to search for</param>
        /// <param name="uris">zero or more URI roots to limit the search to</param>
        /// <param name="exact">if the search should use exact matching</param>
        /// <returns></returns>
        /// <remarks>
        /// field can be one of uri, track_name, album, artist, albumartist, composer,
        /// performer, track_no, genre, date, comment, or any.
        /// If uris is given, the search is limited to results from within the URI roots.
        /// For example passing uris=['file:'] will limit the search to the local backend.
        /// </remarks>
        public static Task<(bool Succeeded, SearchResult[] Result)> Search(
            string queryUri = null,
            string queryTrackName = null,
            string queryAlbum = null,
            string queryArtist = null,
            string queryAlbumArtist = null,
            string queryComposer = null,
            string queryPerformer = null,
            int? queryTrackNo = null,
            string queryGenre = null,
            string queryDate = null,
            string queryComment = null,
            string queryAny = null,

            string[] uris = null,
            bool exact = false
        )
        {
            var query = new Query();

            if (queryUri != null)
                query.Uri = new string[] { queryUri };
            if (queryTrackName != null)
                query.TrackName = new string[] { queryTrackName };
            if (queryAlbum != null)
                query.Album = new string[] { queryAlbum };
            if (queryArtist != null)
                query.Artist = new string[] { queryArtist };
            if (queryAlbumArtist != null)
                query.AlbumArtist = new string[] { queryAlbumArtist };
            if (queryComposer != null)
                query.Composer = new string[] { queryComposer };
            if (queryPerformer != null)
                query.Performer = new string[] { queryPerformer };
            if (queryTrackNo != null)
                query.TrackNo = new int[] { (int)queryTrackNo };
            if (queryGenre != null)
                query.Genre = new string[] { queryGenre };
            if (queryDate != null)
                query.Date = new string[] { queryDate };
            if (queryComment != null)
                query.Comment = new string[] { queryComment };
            if (queryAny != null)
                query.Any = new string[] { queryAny };

            return Library.Search(query, uris, exact);
        }

        /// <summary>
        /// Lookup the given URIs.
        /// </summary>
        /// <param name="uris">track URIs</param>
        /// <returns></returns>
        /// <remarks>
        /// If the URI expands to multiple tracks, the returned list will contain them all.
        /// </remarks>
        public static async Task<(bool Succeeded, Dictionary<string, Track[]> Result)> Lookup(
            string[] uris
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodLookup,
                new
                {
                    uris
                }
            );

            var response = await Library._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JObject.FromObject(response.Result)
                .ToObject<Dictionary<string, Track[]>>();

            return (true, result);
        }

        /// <summary>
        /// Refresh library. Limit to URI and below if an URI is given.
        /// </summary>
        /// <param name="uri">directory or track URI</param>
        /// <returns></returns>
        public static async Task<bool> Refresh(string uri = null)
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodRefresh,
                new
                {
                    uri
                }
            );

            var response = await Library._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lookup the images for the given URIs
        /// </summary>
        /// <param name="uris">list of URIs to find images for</param>
        /// <returns></returns>
        /// <remarks>
        /// Backends can use this to return image URIs for any URI they know
        /// about be it tracks, albums, playlists.
        /// The lookup result is a dictionary mapping the provided URIs to lists of images.
        /// Unknown URIs or URIs the corresponding backend couldn’t find anything
        /// for will simply return an empty list for that URI.
        /// </remarks>
        public static async Task<(bool Succeeded, Dictionary<string, Image[]> Result)> GetImages(
            string[] uris
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodGetImages,
                new
                {
                    uris
                }
            );

            var response = await Library._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JObject.FromObject(response.Result)
                .ToObject<Dictionary<string, Image[]>>();

            return (true, result);
        }

        /// <summary>
        /// List distinct values for a given field from the library.
        /// </summary>
        /// <param name="field">One of track, artist, albumartist, album, composer, performer, date or genre.</param>
        /// <param name="query">Query to use for limiting results, see search() for details about the query format.</param>
        /// <returns></returns>
        /// <remarks>
        /// This has mainly been added to support the list commands the MPD protocol
        /// supports in a more sane fashion.
        /// Other frontends are not recommended to use this method.
        /// </remarks>
        public static async Task<(bool Succeeded, Dictionary<string, string[]> Result)> GetDistinct(
            FieldType field,
            Query query = null
        )
        {
            var request = JsonRpcFactory.CreateRequest(
                Library.MethodGetDistinct,
                new
                {
                    field = Library.GetFieldTypeString(field),
                    query
                }
            );

            var response = await Library._query.Exec(request);

            if (response.Error != null)
            {
                // TODO: Logger実装, Logger.Write(response.Error);
                return (false, null);
            }

            // TODO: 注意、戻り値の型を確認する。
            // 戻り値の型は、[ JObject | JArray | JValue | null ] のどれか。
            // 型が違うとパースエラーになる。
            var result = JObject.FromObject(response.Result)
                .ToObject<Dictionary<string, string[]>>();

            return (true, result);
        }

        /// <summary>
        /// List distinct values for a given field from the library.
        /// </summary>
        /// <param name="field">One of track, artist, albumartist, album, composer, performer, date or genre.</param>
        /// <param name="queryUri">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryTrackName">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAlbum">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryArtist">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAlbumArtist">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryComposer">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryPerformer">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryTrackNo">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryGenre">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryDate">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryComment">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAny">Query to use for limiting results, see search() for details about the query format.</param>
        /// <returns></returns>
        /// <remarks>
        /// This has mainly been added to support the list commands the MPD protocol
        /// supports in a more sane fashion.
        /// Other frontends are not recommended to use this method.
        /// </remarks>
        public static Task<(bool Succeeded, Dictionary<string, string[]> Result)> GetDistinct(
            FieldType field,
            string[] queryUri = null,
            string[] queryTrackName = null,
            string[] queryAlbum = null,
            string[] queryArtist = null,
            string[] queryAlbumArtist = null,
            string[] queryComposer = null,
            string[] queryPerformer = null,
            int[] queryTrackNo = null,
            string[] queryGenre = null,
            string[] queryDate = null,
            string[] queryComment = null,
            string[] queryAny = null
        )
        {
            var query = new Query()
            {
                Uri = queryUri,
                TrackName = queryTrackName,
                Album = queryAlbum,
                Artist = queryArtist,
                AlbumArtist = queryAlbumArtist,
                Composer = queryComposer,
                Performer = queryPerformer,
                TrackNo = queryTrackNo,
                Genre = queryGenre,
                Date = queryDate,
                Comment = queryComment,
                Any = queryAny
            };

            return Library.GetDistinct(field, query);
        }

        /// <summary>
        /// List distinct values for a given field from the library.
        /// </summary>
        /// <param name="field">One of track, artist, albumartist, album, composer, performer, date or genre.</param>
        /// <param name="queryUri">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryTrackName">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAlbum">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryArtist">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAlbumArtist">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryComposer">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryPerformer">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryTrackNo">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryGenre">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryDate">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryComment">Query to use for limiting results, see search() for details about the query format.</param>
        /// <param name="queryAny">Query to use for limiting results, see search() for details about the query format.</param>
        /// <returns></returns>
        /// <remarks>
        /// This has mainly been added to support the list commands the MPD protocol
        /// supports in a more sane fashion.
        /// Other frontends are not recommended to use this method.
        /// </remarks>
        public static Task<(bool Succeeded, Dictionary<string, string[]> Result)> GetDistinct(
            FieldType field,
            string queryUri = null,
            string queryTrackName = null,
            string queryAlbum = null,
            string queryArtist = null,
            string queryAlbumArtist = null,
            string queryComposer = null,
            string queryPerformer = null,
            int? queryTrackNo = null,
            string queryGenre = null,
            string queryDate = null,
            string queryComment = null,
            string queryAny = null
        )
        {
            var query = new Query();

            if (queryUri != null)
                query.Uri = new string[] { queryUri };
            if (queryTrackName != null)
                query.TrackName = new string[] { queryTrackName };
            if (queryAlbum != null)
                query.Album = new string[] { queryAlbum };
            if (queryArtist != null)
                query.Artist = new string[] { queryArtist };
            if (queryAlbumArtist != null)
                query.AlbumArtist = new string[] { queryAlbumArtist };
            if (queryComposer != null)
                query.Composer = new string[] { queryComposer };
            if (queryPerformer != null)
                query.Performer = new string[] { queryPerformer };
            if (queryTrackNo != null)
                query.TrackNo = new int[] { (int)queryTrackNo };
            if (queryGenre != null)
                query.Genre = new string[] { queryGenre };
            if (queryDate != null)
                query.Date = new string[] { queryDate };
            if (queryComment != null)
                query.Comment = new string[] { queryComment };
            if (queryAny != null)
                query.Any = new string[] { queryAny };

            return Library.GetDistinct(field, query);
        }

        private static string GetFieldTypeString(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.Track: return Library.FieldTypeTrack;
                case FieldType.Artist: return Library.FieldTypeArtist;
                case FieldType.AlbumArtist: return Library.FieldTypeAlbumArtist;
                case FieldType.Album: return Library.FieldTypeAlbum;
                case FieldType.Composer: return Library.FieldTypeComposer;
                case FieldType.Performer: return Library.FieldTypePerformer;
                case FieldType.Date: return Library.FieldTypeDate;
                case FieldType.Genre: return Library.FieldTypeGenre;
                default:
                    throw new ArgumentException($"Unexpected FieldType<{fieldType}>");
            }
        }
    }
}
