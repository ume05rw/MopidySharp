using Mopidy.Core;
using Mopidy.Models;
using MopidySharpTest.Bases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class TracklistTest : TestBase
    {
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

            var res4 = await Tracklist.GetTlTracks();
            Assert.True(res4.Succeeded);
            Assert.Contains(res4.Result, e => e.TlId == tlIds[0]);
            Assert.Contains(res4.Result, e => e.TlId == tlIds[1]);
            Assert.Contains(res4.Result, e => e.TlId == tlIds[2]);
            Assert.Contains(res4.Result, e => e.TlId == tlIds[3]);

            var res5 = await Tracklist.Remove(new int[]
            {
                tlIds[0],
                tlIds[1],
                tlIds[2],
                tlIds[3]
            });
            Assert.True(res5.Succeeded);
            Assert.Equal(4, res5.Result.Length);

            var res6 = await Tracklist.GetLength();
            Assert.True(res6.Succeeded);
            Assert.Equal(tracks1.Length - 4, res6.Result);

            var res7 = await Tracklist.GetTlTracks();
            Assert.True(res7.Succeeded);
            Assert.DoesNotContain(res7.Result, e => e.TlId == tlIds[0]);
            Assert.DoesNotContain(res7.Result, e => e.TlId == tlIds[1]);
            Assert.DoesNotContain(res7.Result, e => e.TlId == tlIds[2]);
            Assert.DoesNotContain(res7.Result, e => e.TlId == tlIds[3]);

            Assert.Contains(res7.Result, e => e.Track.Uri == uris[4]);
            Assert.Contains(res7.Result, e => e.Track.Uri == uris[5]);
            Assert.Contains(res7.Result, e => e.Track.Uri == uris[6]);

            var res8 = await Tracklist.Remove(new string[]
            {
                uris[4],
                uris[5],
                uris[6]
            });
            Assert.True(res8.Succeeded);
            Assert.Equal(3, res8.Result.Length);

            var res9 = await Tracklist.GetLength();
            Assert.True(res9.Succeeded);
            Assert.Equal(tracks1.Length - 7, res9.Result);

            var res10 = await Tracklist.GetTlTracks();
            Assert.True(res10.Succeeded);
            Assert.DoesNotContain(res10.Result, e => e.Track.Uri == uris[4]);
            Assert.DoesNotContain(res10.Result, e => e.Track.Uri == uris[5]);
            Assert.DoesNotContain(res10.Result, e => e.Track.Uri == uris[6]);

            Assert.Contains(res10.Result, e => e.TlId == tlIds[7]);
            Assert.Contains(res10.Result, e => e.Track.Uri == uris[8]);

            var res11 = await Tracklist.Remove(tlIds[7]);
            Assert.True(res11.Succeeded);
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
            Assert.Equal(1, res11.Result.Length);
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.

            var res12 = await Tracklist.Remove(uris[8]);
            Assert.True(res12.Succeeded);
#pragma warning disable xUnit2013 // Do not use equality check to check for collection size.
            Assert.Equal(1, res12.Result.Length);
#pragma warning restore xUnit2013 // Do not use equality check to check for collection size.

            var res13 = await Tracklist.GetLength();
            Assert.True(res13.Succeeded);
            Assert.Equal(tracks1.Length - 9, res13.Result);

            var res14 = await Tracklist.GetTlTracks();
            Assert.True(res14.Succeeded);
            Assert.DoesNotContain(res14.Result, e => e.TlId == tlIds[7]);
            Assert.DoesNotContain(res14.Result, e => e.Track.Uri == uris[8]);
        }

        [Fact]
        public async Task ClearTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris1 = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris1);
            Assert.True(res3.Succeeded);

            var res4 = await Tracklist.GetLength();
            Assert.True(res4.Succeeded);
            Assert.Equal(uris1.Length, res4.Result);

            var res5 = await Tracklist.Clear();
            Assert.True(res5);

            var res6 = await Tracklist.GetLength();
            Assert.True(res6.Succeeded);
            Assert.Equal(0, res6.Result);
        }

        [Fact]
        public async Task MoveTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res4 = await Tracklist.Add(uris);
            Assert.True(res4.Succeeded);

            // 2-3 -> index 5
            var res5 = await Tracklist.Move(2, 4, 5);

            var moved = new string[]
            {
                uris[0],
                uris[1],
                uris[4],
                uris[5],
                uris[6],
                uris[2],
                uris[3],
                uris[7],
                uris[8],
                uris[9],
                uris[10]
            };

            var res6 = await Tracklist.GetTlTracks();
            Assert.True(res6.Succeeded);
            Assert.Equal(moved.Length, res6.Result.Length);

            for (var i = 0; i < moved.Length; i++)
            {
                Assert.Equal(moved[i], res6.Result[i].Track.Uri);
            }
        }

        [Fact]
        public async Task ShuffleTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);

            var ordered = new Dictionary<int, string>()
            {
                { 0, uris[0] },
                { 1, uris[1] },
                { 2, uris[2] },
                { 3, uris[3] },
                { 4, uris[4] },
                { 10, uris[10] }
            };
            var unordered = new Dictionary<int, string>()
            {
                { 5, uris[5] },
                { 6, uris[6] },
                { 7, uris[7] },
                { 8, uris[8] },
                { 9, uris[9] },
            };
            var res4 = await Tracklist.Shuffle(5, 10);
            Assert.True(res4);

            var res5 = await Tracklist.GetTlTracks();
            Assert.True(res5.Succeeded);
            Assert.Equal(uris.Length, res5.Result.Length);

            foreach (var pair in ordered)
            {
                Assert.Equal(ordered[pair.Key], res5.Result[pair.Key].Track.Uri);
            }

            var shuffled = false;
            foreach (var pair in unordered)
            {
                if (pair.Value != res5.Result[pair.Key].Track.Uri)
                {
                    shuffled = true;
                    break;
                }
            }
            Assert.True(shuffled);
        }

        [Fact]
        public async Task GetTlTracksTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Tracklist.GetTlTracks();
            Assert.True(res2.Succeeded);
            Assert.Empty(res2.Result);

            var res3 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res3.Succeeded);
            Assert.True(1 <= res3.Result.Length);
            Assert.True(1 <= res3.Result.First().Tracks.Length);
            var uris = res3.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res4 = await Tracklist.Add(uris);
            Assert.True(res4.Succeeded);

            var res5 = await Tracklist.GetTlTracks();
            Assert.True(res5.Succeeded);
            Assert.Equal(uris.Length, res5.Result.Length);

            for (var i = 0; i < uris.Length; i++)
            {
                Assert.Equal(uris[i], res5.Result[i].Track.Uri);
            }
        }

        [Fact]
        public async Task IndexTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);

            var res4 = await Tracklist.GetTlTracks();
            Assert.True(res4.Succeeded);
            Assert.Equal(uris.Length, res4.Result.Length);

            for (var i = 0; i < uris.Length; i++)
            {
                var res5 = await Tracklist.Index(tlId: res4.Result[i].TlId);
                Assert.Equal(uris[(int)res5.Result], res4.Result[i].Track.Uri);
            }
        }

        [Fact]
        public async Task GetVersionTest()
        {
            var res1 = await Tracklist.GetVersion();
            Assert.True(res1.Succeeded);
            Assert.True(0 <= res1.Result);

            var res2 = await Tracklist.Clear();
            Assert.True(res2);

            var res3 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res3.Succeeded);
            Assert.True(1 <= res3.Result.Length);
            Assert.True(1 <= res3.Result.First().Tracks.Length);
            var uris = res3.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res4 = await Tracklist.Add(uris);
            Assert.True(res4.Succeeded);

            var res5 = await Tracklist.GetVersion();
            Assert.True(res5.Succeeded);
            Assert.True(res1.Result < res5.Result);
        }

        [Fact]
        public async Task GetLengthTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Tracklist.GetLength();
            Assert.True(res2.Succeeded);
            Assert.Equal(0, res2.Result);

            var res3 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res3.Succeeded);
            Assert.True(1 <= res3.Result.Length);
            Assert.True(1 <= res3.Result.First().Tracks.Length);
            var uris = res3.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res4 = await Tracklist.Add(uris);
            Assert.True(res4.Succeeded);

            var res5 = await Tracklist.GetLength();
            Assert.True(res5.Succeeded);
            Assert.Equal(uris.Length, res5.Result);
        }

        [Fact]
        public async Task GetTracksTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Tracklist.GetTracks();
            Assert.True(res2.Succeeded);
            Assert.Empty(res2.Result);

            var res3 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res3.Succeeded);
            Assert.True(1 <= res3.Result.Length);
            Assert.True(1 <= res3.Result.First().Tracks.Length);
            var uris = res3.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res4 = await Tracklist.Add(uris);
            Assert.True(res4.Succeeded);
            Assert.Equal(uris.Length, res4.Result.Length);

            var res5 = await Tracklist.GetTracks();
            Assert.True(res5.Succeeded);
            Assert.Equal(uris.Length, res5.Result.Length);

            for (var i = 0; i < uris.Length; i++)
            {
                Assert.Equal(uris[i], res5.Result[i].Uri);
            }
        }

        [Fact]
        public async Task SliceTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);
            Assert.Equal(uris.Length, res3.Result.Length);

            var sliced = new string[]
            {
                uris[5],
                uris[6],
                uris[7],
                uris[8],
            };

            var res4 = await Tracklist.Slice(5, 9);
            Assert.True(res4.Succeeded);
            Assert.Equal(4, res4.Result.Length);

            for (var i = 0; i < sliced.Length; i++)
            {
                Assert.Equal(sliced[i], res4.Result[i].Track.Uri);
            }

            var res5 = await Tracklist.GetTlTracks();
            Assert.True(res5.Succeeded);
            Assert.Equal(uris.Length, res5.Result.Length);

            for (var i = 0; i < uris.Length; i++)
            {
                Assert.Equal(uris[i], res5.Result[i].Track.Uri);
            }
        }

        [Fact]
        public async Task FilterTest()
        {
            var res1 = await Tracklist.Clear();
            Assert.True(res1);

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            Assert.True(1 <= res2.Result.First().Tracks.Length);
            var uris = res2.Result.First().Tracks
                .OrderBy(e => e.TrackNo)
                .Select(e => e.Uri)
                .ToArray();

            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);
            Assert.Equal(uris.Length, res3.Result.Length);

            var filtered1 = new string[]
            {
                uris[1],
                uris[3],
                uris[5],
                uris[7],
            };

            var criteria = new Tracklist.Criteria();
            criteria.TlId.Add(res3.Result[1].TlId);
            criteria.TlId.Add(res3.Result[3].TlId);
            criteria.TlId.Add(res3.Result[5].TlId);
            criteria.TlId.Add(res3.Result[7].TlId);
            //criteria.Uri.Add("local:track:");

            var res4 = await Tracklist.Filter(criteria);
            Assert.True(res4.Succeeded);
            Assert.Equal(filtered1.Length, res4.Result.Length);
            for (var i = 0; i < filtered1.Length; i++)
            {
                Assert.Equal(filtered1[i], res4.Result[i].Track.Uri);
            }

            criteria.Clear();
            criteria.Uri.Add(res3.Result[1].Track.Uri);
            criteria.Uri.Add(res3.Result[3].Track.Uri);
            criteria.Uri.Add(res3.Result[5].Track.Uri);
            criteria.Uri.Add(res3.Result[7].Track.Uri);

            var res5 = await Tracklist.Filter(criteria);
            Assert.True(res5.Succeeded);
            Assert.Equal(filtered1.Length, res5.Result.Length);
            for (var i = 0; i < filtered1.Length; i++)
            {
                Assert.Equal(filtered1[i], res5.Result[i].Track.Uri);
            }

            var res6 = await Tracklist.Filter(tlId: new int[]
            {
                res3.Result[1].TlId,
                res3.Result[3].TlId,
                res3.Result[5].TlId,
                res3.Result[7].TlId,
            });
            Assert.True(res6.Succeeded);
            Assert.Equal(filtered1.Length, res6.Result.Length);
            for (var i = 0; i < filtered1.Length; i++)
            {
                Assert.Equal(filtered1[i], res6.Result[i].Track.Uri);
            }

            var res7 = await Tracklist.Filter(uri: new string[]
            {
                res3.Result[1].Track.Uri,
                res3.Result[3].Track.Uri,
                res3.Result[5].Track.Uri,
                res3.Result[7].Track.Uri,
            });
            Assert.True(res7.Succeeded);
            Assert.Equal(filtered1.Length, res7.Result.Length);
            for (var i = 0; i < filtered1.Length; i++)
            {
                Assert.Equal(filtered1[i], res7.Result[i].Track.Uri);
            }

            var res8 = await Tracklist.Filter(tlId: res3.Result[1].TlId);
            Assert.True(res8.Succeeded);
            Assert.Single(res8.Result);
            Assert.Equal(res3.Result[1].Track.Uri, res8.Result[0].Track.Uri);

            var res9 = await Tracklist.Filter(uri: res3.Result[3].Track.Uri);
            Assert.True(res9.Succeeded);
            Assert.Single(res9.Result);
            Assert.Equal(res3.Result[3].TlId, res9.Result[0].TlId);


            var res10 = await Tracklist.GetTlTracks();
            Assert.True(res10.Succeeded);
            Assert.Equal(uris.Length, res10.Result.Length);

            for (var i = 0; i < uris.Length; i++)
            {
                Assert.Equal(uris[i], res10.Result[i].Track.Uri);
            }
        }

        [Fact]
        public async Task GetEotTlIdTest()
        {
            var res = await Tracklist.GetEotTlId();
            Assert.True(res.Succeeded);

            if (res.Result != null)
                Assert.True(0 <= res.Result);
        }

        [Fact]
        public async Task GetNextTlIdTest()
        {
            var res = await Tracklist.GetNextTlId();
            Assert.True(res.Succeeded);

            if (res.Result != null)
                Assert.True(0 <= res.Result);
        }

        [Fact]
        public async Task GetPreviousTlIdTest()
        {
            var res = await Tracklist.GetPreviousTlId();
            Assert.True(res.Succeeded);

            if (res.Result != null)
                Assert.True(0 <= res.Result);
        }

        [Fact]
        public async Task GetConsumeTest()
        {
            var res = await Tracklist.GetConsume();
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task SetConsumeTest()
        {
            var res1 = await Tracklist.GetConsume();
            Assert.True(res1.Succeeded);

            var res2 = await Tracklist.SetConsume(!res1.Result);
            Assert.True(res2);

            var res3 = await Tracklist.GetConsume();
            Assert.True(res3.Succeeded);
            Assert.True(res1.Result != res3.Result);
        }

        [Fact]
        public async Task GetRandomTest()
        {
            var res = await Tracklist.GetRandom();
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task SetRandomTest()
        {
            var res1 = await Tracklist.GetRandom();
            Assert.True(res1.Succeeded);

            var res2 = await Tracklist.SetRandom(!res1.Result);
            Assert.True(res2);

            var res3 = await Tracklist.GetRandom();
            Assert.True(res3.Succeeded);
            Assert.True(res1.Result != res3.Result);
        }

        [Fact]
        public async Task GetRepeatTest()
        {
            var res = await Tracklist.GetRepeat();
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task SetRepeatTest()
        {
            var res1 = await Tracklist.GetRepeat();
            Assert.True(res1.Succeeded);

            var res2 = await Tracklist.SetRepeat(!res1.Result);
            Assert.True(res2);

            var res3 = await Tracklist.GetRepeat();
            Assert.True(res3.Succeeded);
            Assert.True(res1.Result != res3.Result);
        }

        [Fact]
        public async Task GetSingleTest()
        {
            var res = await Tracklist.GetSingle();
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task SetSingleTest()
        {
            var res1 = await Tracklist.GetSingle();
            Assert.True(res1.Succeeded);

            var res2 = await Tracklist.SetSingle(!res1.Result);
            Assert.True(res2);

            var res3 = await Tracklist.GetSingle();
            Assert.True(res3.Succeeded);
            Assert.True(res1.Result != res3.Result);
        }
    }
}
