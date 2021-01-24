using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Mopidy.Core;
using System.Linq;

namespace MopidySharpTest.Core
{
    public class TracklistTest : TestBase
    {
        [Fact]
        public async Task ClearTest()
        {
            var res = await Tracklist.Clear();
            Assert.True(res);
        }

        [Fact]
        public async Task GetLengthTest()
        {
            var res = await Tracklist.GetLength();
            Assert.True(res.Succeeded);
            Assert.True(0 <= res.Result);
        }

        [Fact]
        public async Task AddTest()
        {
            var res1 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res1.Succeeded);
            Assert.True(1 <= res1.Result.Length);
            var tracks = res1.Result.First().Tracks;
            Assert.True(1 <= tracks.Length);
            var uris = tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res2 = await Tracklist.Clear();
            Assert.True(res2);

            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);
            Assert.Equal(tracks.Length, res3.Result.Length);

            for(var i = 0; i < tracks.Length; i++)
            {
                var track = tracks[i];
                var result = res3.Result[i];
                Assert.Equal(track.Uri, result.Track.Uri);
            }

            var res4 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "The Whole Thing's Started",
                queryTrackName: "Do It Again"
            );
            Assert.True(res4.Succeeded);
            Assert.True(1 <= res4.Result.Length);
            var tracks2 = res4.Result.First().Tracks;
            Assert.True(1 == tracks2.Length);
            var insertUri = tracks2[0].Uri;

            var res5 = await Tracklist.Add(insertUri, 3);
            Assert.True(res5.Succeeded);

            var res6 = await Tracklist.GetLength();
            Assert.Equal(tracks.Length + 1, res6.Result);
        }
    }
}
