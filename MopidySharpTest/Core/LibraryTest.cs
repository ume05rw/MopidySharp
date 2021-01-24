using Mopidy.Core;
using MopidySharpTest.Bases;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class LibraryTest : TestBase
    {
        [Fact]
        public async Task BrowseTest()
        {
            var res1 = await Library.Browse(null);
            Assert.True(res1.Succeeded);
            var result1 = res1.Result;

            foreach (var resRef in result1)
            {
                var res2 = await Library.Browse(resRef.Uri);
                Assert.True(res2.Succeeded);

                foreach (var resRef2 in res2.Result)
                {
                    var res3 = await Library.Browse(resRef2.Uri);
                    Assert.True(res3.Succeeded);
                }
            }

            var res4 = await Library.Browse("local:directory?type=album&albumartist='Various Artists'");
        }

        [Fact]
        public void SearchArgumentSerializeTest()
        {
            var query1 = new Library.Query();
            query1.Uri.Add("local:track");
            query1.TrackName.Add("Feel The Breeze");
            query1.Album.Add("Strangers In Love");
            query1.Artist.Add("Air Supply");
            query1.AlbumArtist.Add("Air Supply");
            query1.Composer.Add("Air Supply");
            query1.Performer.Add("Air Supply");
            query1.TrackNo.Add(1);
            query1.Genre.Add("AOR");
            query1.Date.Add("1976");
            query1.Comment.Add("Air Supply");
            query1.Any.Add("Air Supply");

            string json = null;
            try
            {
                json = JsonConvert.SerializeObject(query1);
            }
            catch (Exception)
            {
                Assert.True(false, "Query Serialize Error");
            }
        }

        [Fact]
        public async Task SearchByClassTest()
        {
            var query = new Library.Query();
            query.Uri.Add("local:track");
            query.TrackName.Add("Feel The Breeze");
            query.Album.Add("Strangers In Love");
            query.Artist.Add("Air Supply");
            query.AlbumArtist.Add("Air Supply");
            query.Composer.Add("Air Supply");
            query.Performer.Add("Air Supply");
            query.TrackNo.Add(1);
            query.Genre.Add("AOR");
            query.Date.Add("1976");
            query.Comment.Add("Air Supply");
            query.Any.Add("Air Supply");

            var res1 = await Library.Search(
                query: query,
                uris: new string[] { "local:track" },
                exact: true
            );
            Assert.True(res1.Succeeded);

            query.Clear();
            query.TrackName.Add("Feel The Breeze");
            query.Album.Add("Strangers In Love");
            query.Artist.Add("Air Supply");
            query.AlbumArtist.Add("Air Supply");
            query.TrackNo.Add(1);
            query.Genre.Add("AOR");
            query.Date.Add("1976");

            var res2 = await Library.Search(
                query: query
            );
            Assert.True(res2.Succeeded);

            query.Clear();
            query.Uri.Add("local:track");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.TrackName.Add("Feel The Breeze");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Album.Add("Strangers In Love");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Artist.Add("Air Supplym");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.AlbumArtist.Add("Air Supply");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Composer.Add("Air Supply");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Performer.Add("Air Supply");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.TrackNo.Add(1);
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Genre.Add("AOR");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Date.Add("1976");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Comment.Add("Air Supply");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.Any.Add("Air Supply");
            Assert.True((await Library.Search(query)).Succeeded);

            query.Clear();
            query.TrackNo.Add(1);
            var res3 = await Library.Search(query);
            Assert.True(res3.Succeeded);
        }

        [Fact]
        public async Task SearchByArrayArgsTest()
        {
            var res1 = await Library.Search(
                queryUri: new string[] { "local:track" },
                queryTrackName: new string[] { "Feel The Breeze" },
                queryAlbum: new string[] { "Strangers In Love" },
                queryArtist: new string[] { "Air Supply" },
                queryAlbumArtist: new string[] { "Air Supply" },
                queryComposer: new string[] { "Air Supply" },
                queryPerformer: new string[] { "Air Supply" },
                queryTrackNo: new int[] { 1 },
                queryGenre: new string[] { "AOR" },
                queryDate: new string[] { "1976" },
                queryComment: new string[] { "Air Supply" },
                queryAny: new string[] { "Air Supply" },

                uris: new string[] { "local:track" },
                exact: true
            );
            Assert.True(res1.Succeeded);

            var res2 = await Library.Search(
                queryTrackName: new string[] { "Feel The Breeze" },
                queryAlbum: new string[] { "Strangers In Love" },
                queryArtist: new string[] { "Air Supply" },
                queryAlbumArtist: new string[] { "Air Supply" },
                queryTrackNo: new int[] { 1 },
                queryGenre: new string[] { "AOR" },
                queryDate: new string[] { "1976" }
            );
            Assert.True(res2.Succeeded);

            Assert.True((await Library.Search(queryUri: new string[] { "local:track" })).Succeeded);
            Assert.True((await Library.Search(queryTrackName: new string[] { "Feel The Breeze" })).Succeeded);
            Assert.True((await Library.Search(queryAlbum: new string[] { "Strangers In Love" })).Succeeded);
            Assert.True((await Library.Search(queryArtist: new string[] { "Air Supply" })).Succeeded);
            Assert.True((await Library.Search(queryAlbumArtist: new string[] { "Air Supply" })).Succeeded);
            Assert.True((await Library.Search(queryComposer: new string[] { "Air Supply" })).Succeeded);
            Assert.True((await Library.Search(queryPerformer: new string[] { "Air Supply" })).Succeeded);
            Assert.True((await Library.Search(queryTrackNo: new int[] { 1 })).Succeeded);
            Assert.True((await Library.Search(queryGenre: new string[] { "AOR" })).Succeeded);
            Assert.True((await Library.Search(queryDate: new string[] { "1976" })).Succeeded);
            Assert.True((await Library.Search(queryComment: new string[] { "Air Supply" })).Succeeded);
            Assert.True((await Library.Search(queryAny: new string[] { "Air Supply" })).Succeeded);
        }

        [Fact]
        public async Task SearchBySingleArgsTest()
        {
            var res1 = await Library.Search(
                queryUri: "local:track" ,
                queryTrackName: "Feel The Breeze",
                queryAlbum: "Air Supply",
                queryArtist: "Air Supply",
                queryAlbumArtist: "Air Supply",
                queryComposer: "Air Supply",
                queryPerformer: "Air Supply",
                queryTrackNo: 1,
                queryGenre: "AOR",
                queryDate: "1976",
                queryComment: "Air Supply",
                queryAny: "Air Supply",

                uris: new string[] { "local:track" },
                exact: true
            );
            Assert.True(res1.Succeeded);

            var res2 = await Library.Search(
                queryTrackName: "Feel The Breeze",
                queryAlbum: "Strangers In Love",
                queryArtist: "Air Supply",
                queryAlbumArtist: "Air Supply",
                queryTrackNo: 1,
                queryGenre: "AOR",
                queryDate: "1976"
            );
            Assert.True(res2.Succeeded);

            Assert.True((await Library.Search(queryUri: "local:track")).Succeeded);
            Assert.True((await Library.Search(queryTrackName: "Feel The Breeze")).Succeeded);
            Assert.True((await Library.Search(queryAlbum: "Strangers In Love")).Succeeded);
            Assert.True((await Library.Search(queryArtist: "Air Supply")).Succeeded);
            Assert.True((await Library.Search(queryAlbumArtist: "Air Supply")).Succeeded);
            Assert.True((await Library.Search(queryComposer: "Air Supply")).Succeeded);
            Assert.True((await Library.Search(queryPerformer: "Air Supply")).Succeeded);
            Assert.True((await Library.Search(queryTrackNo: 1)).Succeeded);
            Assert.True((await Library.Search(queryGenre: "AOR")).Succeeded);
            Assert.True((await Library.Search(queryDate: "1976")).Succeeded);
            Assert.True((await Library.Search(queryComment: "Air Supply")).Succeeded);
            Assert.True((await Library.Search(queryAny: "Air Supply")).Succeeded);
        }

        [Fact]
        public async Task LookupTest()
        {
            var res1 = await Library.Lookup(new string[]
            {
                "local:track?artist='Air Supply'",
                "local:track?genre='AOR'"
            });
            Assert.True(res1.Succeeded);

            var res2 = await Library.Lookup(new string[]
            {
                "local:track?artist='Air Supply'"
            });
            Assert.True(res2.Succeeded);

            var res3 = await Library.Lookup("local:track?artist=hello");
            Assert.True(res2.Succeeded);
        }

        [Fact]
        public async Task RefreshTest()
        {
            var res1 = await Library.Search(queryArtist: "Air Supply");
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
            var first = res1.Result.First().Tracks.First();

            var res2 = await Library.Refresh(first.Uri);
            Assert.True(res2);

            //var res3 = await Library.Refresh();
            //Assert.True(res3);
        }

        [Fact]
        public async Task GetImagesMultiTest()
        {
            var res1 = await Library.Search(queryArtist: "Air Supply");
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
            Assert.True(3 <= res1.Result.First().Tracks.Length);
            var albumUris = res1.Result.First().Tracks
                .GroupBy(e => e.Album?.Uri)
                .Select(e => e.Key)
                .Where(e => e != null)
                .Take(3)
                .ToArray();

            var res2 = await Library.GetImages(albumUris);
            Assert.True(res2.Succeeded);
            var result = res2.Result;

            foreach (var uri in albumUris)
            {
                Assert.True(result.ContainsKey(uri));
                Assert.True(0 <= result[uri].Length);
            }
        }

        [Fact]
        public async Task GetImagesSingleTest()
        {
            var res1 = await Library.Search(queryArtist: "Air Supply");
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
            Assert.True(3 <= res1.Result.First().Tracks.Length);
            var albumUri = res1.Result.First().Tracks
                .GroupBy(e => e.Album?.Uri)
                .Select(e => e.Key)
                .Where(e => e != null)
                .First();

            var res2 = await Library.GetImages(albumUri);
            Assert.True(res2.Succeeded);
            Assert.True(0 <= res2.Result.Length);
        }

        //[Fact]
        //public async Task GetDistinctByClassTest()
        //{
        //    var query = new Library.Query();
        //    query.Uri.Add("local:track");
        //    query.TrackName.Add("Feel The Breeze");
        //    query.Album.Add("Strangers In Love");
        //    query.Artist.Add("Air Supply");
        //    query.AlbumArtist.Add("Air Supply");
        //    query.Composer.Add("Air Supply");
        //    query.Performer.Add("Air Supply");
        //    query.TrackNo.Add(1);
        //    query.Genre.Add("AOR");
        //    query.Date.Add("1976");
        //    query.Comment.Add("Air Supply");
        //    query.Any.Add("Air Supply");

        //    var res1 = await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        query: query
        //    );
        //    Assert.True(res1.Succeeded);

        //    query.Clear();
        //    query.TrackName.Add("Feel The Breeze");
        //    query.Album.Add("Strangers In Love");
        //    query.Artist.Add("Air Supply");
        //    query.AlbumArtist.Add("Air Supply");
        //    query.TrackNo.Add(1);
        //    query.Genre.Add("AOR");
        //    query.Date.Add("1976");

        //    var res2 = await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        query: query
        //    );
        //    Assert.True(res2.Succeeded);

        //    query.Clear();
        //    query.Uri.Add("local:track");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Album, query)).Succeeded);

        //    query.Clear();
        //    query.TrackName.Add("Feel The Breeze");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.AlbumArtist , query)).Succeeded);

        //    query.Clear();
        //    query.Album.Add("Strangers In Love");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Artist, query)).Succeeded);

        //    query.Clear();
        //    query.Artist.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Composer, query)).Succeeded);

        //    query.Clear();
        //    query.AlbumArtist.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Date, query)).Succeeded);

        //    query.Clear();
        //    query.Composer.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Genre, query)).Succeeded);

        //    query.Clear();
        //    query.Performer.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Performer, query)).Succeeded);

        //    query.Clear();
        //    query.TrackNo.Add(1);
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Track, query)).Succeeded);

        //    query.Clear();
        //    query.Genre.Add("AOR");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Album, query)).Succeeded);

        //    query.Clear();
        //    query.Date.Add("1976");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.AlbumArtist, query)).Succeeded);

        //    query.Clear();
        //    query.Comment.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Artist, query)).Succeeded);

        //    query.Clear();
        //    query.Any.Add("Air Supply");
        //    Assert.True((await Library.GetDistinct(Library.FieldType.Composer, query)).Succeeded);
        //}

        //[Fact]
        //public async Task GetDistinctByArrayArgsTest()
        //{
        //    var res1 = await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        queryUri: new string[] { "local:track" },
        //        queryTrackName: new string[] { "Feel The Breeze" },
        //        queryAlbum: new string[] { "Strangers In Love" },
        //        queryArtist: new string[] { "Air Supply" },
        //        queryAlbumArtist: new string[] { "Air Supply" },
        //        queryComposer: new string[] { "Air Supply" },
        //        queryPerformer: new string[] { "Air Supply" },
        //        queryTrackNo: new int[] { 1 },
        //        queryGenre: new string[] { "AOR" },
        //        queryDate: new string[] { "1976" },
        //        queryComment: new string[] { "Air Supply" },
        //        queryAny: new string[] { "Air Supply" }
        //    );
        //    Assert.True(res1.Succeeded);

        //    var res2 = await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        queryTrackName: new string[] { "Feel The Breeze" },
        //        queryAlbum: new string[] { "Strangers In Love" },
        //        queryArtist: new string[] { "Air Supply" },
        //        queryAlbumArtist: new string[] { "Air Supply" },
        //        queryTrackNo: new int[] { 1 },
        //        queryGenre: new string[] { "AOR" },
        //        queryDate: new string[] { "1976" }
        //    );
        //    Assert.True(res2.Succeeded);

        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        queryUri: new string[] { "local:track" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.AlbumArtist,
        //        queryTrackName: new string[] { "Feel The Breeze" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Artist,
        //        queryAlbum: new string[] { "Strangers In Love" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Composer,
        //        queryArtist: new string[] { "Air Supply" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Date,
        //        queryAlbumArtist: new string[] { "Air Supply" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Genre,
        //        queryComposer: new string[] { "Air Supply" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Performer,
        //        queryPerformer: new string[] { "Air Supply" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Track,
        //        queryTrackNo: new int[] { 1 }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Album,
        //        queryGenre: new string[] { "AOR" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.AlbumArtist,
        //        queryDate: new string[] { "1976" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Artist,
        //        queryComment: new string[] { "Air Supply" }
        //    )).Succeeded);
        //    Assert.True((await Library.GetDistinct(
        //        field: Library.FieldType.Composer,
        //        queryAny: new string[] { "Air Supply" }
        //    )).Succeeded);
        //}

        //[Fact]
        //public async Task GetDistinctBySingleArgsTest()
        //{
        //    var res1 = await Library.Search(
        //        queryUri: "local:track",
        //        queryTrackName: "Feel The Breeze",
        //        queryAlbum: "Strangers In Love",
        //        queryArtist: "Air Supply",
        //        queryAlbumArtist: "Air Supply",
        //        queryComposer: "Air Supply",
        //        queryPerformer: "Air Supply",
        //        queryTrackNo: 1,
        //        queryGenre: "AOR",
        //        queryDate: "1976",
        //        queryComment: "Air Supply",
        //        queryAny: "Air Supply",

        //        uris: new string[] { "local:track" },
        //        exact: true
        //    );
        //    Assert.True(res1.Succeeded);

        //    var res2 = await Library.Search(
        //        queryTrackName: "Feel The Breeze",
        //        queryAlbum: "Strangers In Love",
        //        queryArtist: "Air Supply",
        //        queryAlbumArtist: "Air Supply",
        //        queryTrackNo: 1,
        //        queryGenre: "AOR",
        //        queryDate: "1976"
        //    );
        //    Assert.True(res2.Succeeded);

        //    Assert.True((await Library.Search(queryUri: "local:track")).Succeeded);
        //    Assert.True((await Library.Search(queryTrackName: "Feel The Breeze")).Succeeded);
        //    Assert.True((await Library.Search(queryAlbum: "Strangers In Love")).Succeeded);
        //    Assert.True((await Library.Search(queryArtist: "Air Supply")).Succeeded);
        //    Assert.True((await Library.Search(queryAlbumArtist: "Air Supply")).Succeeded);
        //    Assert.True((await Library.Search(queryComposer: "Air Supply")).Succeeded);
        //    Assert.True((await Library.Search(queryPerformer: "Air Supply")).Succeeded);
        //    Assert.True((await Library.Search(queryTrackNo: 1)).Succeeded);
        //    Assert.True((await Library.Search(queryGenre: "AOR")).Succeeded);
        //    Assert.True((await Library.Search(queryDate: "1976")).Succeeded);
        //    Assert.True((await Library.Search(queryComment: "Air Supply")).Succeeded);
        //    Assert.True((await Library.Search(queryAny: "Air Supply")).Succeeded);
        //}
    }
}
