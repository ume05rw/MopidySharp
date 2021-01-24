using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Mopidy.Core;
using System.Linq;
using Mopidy.Models;

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
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            var tracks1 = res2.Result.First().Tracks;
            Assert.True(1 <= tracks1.Length);
            var uris1 = tracks1
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris1);
            Assert.True(res3.Succeeded);
            Assert.Equal(tracks1.Length, res3.Result.Length);
            for(var i = 0; i < tracks1.Length; i++)
            {
                var track = tracks1[i];
                var result = res3.Result[i];
                Assert.Equal(track.Uri, result.Track.Uri);
            }

            var res4 = await Library.Search(
                queryArtist: "Abba",
                queryAlbum: "Waterloo"
            );
            Assert.True(res4.Succeeded);
            Assert.True(1 <= res4.Result.Length);
            var tracks2 = res4.Result.First().Tracks;
            Assert.True(1 <= tracks2.Length);
            var uris2 = tracks2
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res5 = await Tracklist.Add(uris2, 0);
            Assert.True(res5.Succeeded);
            Assert.Equal(tracks2.Length, res5.Result.Length);
            for (var i = 0; i < tracks2.Length; i++)
            {
                var track = tracks2[i];
                var result = res5.Result[i];
                Assert.Equal(track.Uri, result.Track.Uri);
            }

            var res6 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "The Whole Thing's Started",
                queryTrackName: "Do It Again"
            );
            Assert.True(res6.Succeeded);
            Assert.True(1 <= res6.Result.Length);
            var tracks3 = res6.Result.First().Tracks;
            Assert.True(1 == tracks3.Length);
            var insertUri = tracks3[0].Uri;

            var res7 = await Tracklist.Add(insertUri, 3);
            Assert.True(res7.Succeeded);

            var res8 = await Tracklist.GetLength();
            Assert.Equal(tracks1.Length + tracks2.Length + 1, res8.Result);

            var allTracks = new List<Track>();
            allTracks.AddRange(tracks2);
            allTracks.AddRange(tracks1);
            allTracks.Insert(3, tracks3[0]);

            var res9 = await Tracklist.GetTlTracks();
            Assert.True(res9.Succeeded);
            Assert.Equal(allTracks.Count, res9.Result.Length);

            for(var i = 0; i < allTracks.Count; i++)
            {
                var track = allTracks[i];
                var tlTrack = res9.Result[i];

                Assert.Equal(track.Uri, tlTrack.Track.Uri);
            }
        }


        [Fact]
        public async Task RemoveTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            var tracks1 = res2.Result.First().Tracks;
            Assert.True(1 <= tracks1.Length);
            var uris1 = tracks1
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris1);
            Assert.True(res3.Succeeded);

            var tlIds = res3.Result
                .Select(e => e.TlId)
                .ToArray();
            var uris = res3.Result
                .Select(e => e.Track.Uri)
                .ToArray();

            var res31 = await Tracklist.GetTlTracks();
            Assert.True(res31.Succeeded);
            Assert.Contains(res31.Result, e => e.TlId == tlIds[0]);
            Assert.Contains(res31.Result, e => e.TlId == tlIds[1]);
            Assert.Contains(res31.Result, e => e.TlId == tlIds[2]);
            Assert.Contains(res31.Result, e => e.TlId == tlIds[3]);

            var res4 = await Tracklist.Remove(new int[]
            {
                tlIds[0],
                tlIds[1],
                tlIds[2],
                tlIds[3]
            });
            Assert.True(res4.Succeeded);
            Assert.Equal(4, res4.Result.Length);

            var res41 = await Tracklist.GetLength();
            Assert.True(res41.Succeeded);
            Assert.Equal(tracks1.Length - 4, res41.Result);

            var res42 = await Tracklist.GetTlTracks();
            Assert.True(res42.Succeeded);
            Assert.DoesNotContain(res42.Result, e => e.TlId == tlIds[0]);
            Assert.DoesNotContain(res42.Result, e => e.TlId == tlIds[1]);
            Assert.DoesNotContain(res42.Result, e => e.TlId == tlIds[2]);
            Assert.DoesNotContain(res42.Result, e => e.TlId == tlIds[3]);

            Assert.Contains(res42.Result, e => e.Track.Uri == uris[4]);
            Assert.Contains(res42.Result, e => e.Track.Uri == uris[5]);
            Assert.Contains(res42.Result, e => e.Track.Uri == uris[6]);

            var res5 = await Tracklist.Remove(new string[]
            {
                uris[4],
                uris[5],
                uris[6]
            });
            Assert.True(res5.Succeeded);
            Assert.Equal(3, res5.Result.Length);

            var res51 = await Tracklist.GetLength();
            Assert.True(res51.Succeeded);
            Assert.Equal(tracks1.Length - 7, res51.Result);

            var res52 = await Tracklist.GetTlTracks();
            Assert.True(res52.Succeeded);
            Assert.DoesNotContain(res52.Result, e => e.Track.Uri == uris[4]);
            Assert.DoesNotContain(res52.Result, e => e.Track.Uri == uris[5]);
            Assert.DoesNotContain(res52.Result, e => e.Track.Uri == uris[6]);

            Assert.Contains(res52.Result, e => e.TlId == tlIds[7]);
            Assert.Contains(res52.Result, e => e.Track.Uri == uris[8]);

            var res8 = await Tracklist.Remove(tlIds[7]);
            Assert.True(res8.Succeeded);
            Assert.Equal(1, res8.Result.Length);

            var res9 = await Tracklist.Remove(uris[8]);
            Assert.True(res9.Succeeded);
            Assert.Equal(1, res9.Result.Length);

            var res91 = await Tracklist.GetLength();
            Assert.True(res91.Succeeded);
            Assert.Equal(tracks1.Length - 9, res91.Result);

            var res92 = await Tracklist.GetTlTracks();
            Assert.True(res92.Succeeded);
            Assert.DoesNotContain(res92.Result, e => e.TlId == tlIds[7]);
            Assert.DoesNotContain(res92.Result, e => e.Track.Uri == uris[8]);
        }
    }
}
