using Mopidy.Core;
using MopidySharpTest.Bases;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class PlaybackTest : TestBase
    {
        private const int WaitMsec = 1000;

        [Fact]
        public async Task PlayNextPreviousStopPauseResumeTest()
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

            var res4 = await Playback.GetState();
            Assert.True(res4.Succeeded);

            if (res4.Result != Playback.PlaybackState.Stopped)
            {
                var res5 = await Playback.Stop();
                Assert.True(res5);
            }

            var res6 = await Playback.Play(res3.Result[3].TlId);
            Assert.True(res6);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res7 = await Playback.GetState();
            Assert.True(res7.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res7.Result);

            var res8 = await Playback.Stop();
            Assert.True(res8);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res9 = await Playback.Play();
            Assert.True(res9);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res10 = await Playback.GetState();
            Assert.True(res10.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res10.Result);

            var res11 = await Playback.Next();
            Assert.True(res11);

            var res12 = await Playback.GetState();
            Assert.True(res12.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res12.Result);

            var res13 = await Playback.Previous();
            Assert.True(res13);

            var res14 = await Playback.GetState();
            Assert.True(res14.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res14.Result);

            var res15 = await Playback.Pause();
            Assert.True(res15);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res16 = await Playback.GetState();
            Assert.True(res16.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res16.Result);

            var res17 = await Playback.Next();
            Assert.True(res17);

            var res18 = await Playback.GetState();
            Assert.True(res18.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res18.Result);

            var res19 = await Playback.Previous();
            Assert.True(res19);

            var res20 = await Playback.GetState();
            Assert.True(res20.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res20.Result);

            var res21 = await Playback.Resume();
            Assert.True(res21);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res22 = await Playback.GetState();
            Assert.True(res22.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res22.Result);

            var res23 = await Playback.Stop();
            Assert.True(res21);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res24 = await Playback.GetState();
            Assert.True(res24.Succeeded);
            Assert.Equal(Playback.PlaybackState.Stopped, res24.Result);
        }

        [Fact]
        public async Task SeekTest()
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

            var res4 = await Playback.GetState();
            Assert.True(res4.Succeeded);

            if (res4.Result != Playback.PlaybackState.Stopped)
            {
                var res5 = await Playback.Stop();
                Assert.True(res5);
            }

            await Task.Delay(PlaybackTest.WaitMsec);

            var res6 = await Playback.Play(res3.Result[4].TlId);
            Assert.True(res6);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res7 = await Playback.GetState();
            Assert.True(res7.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res7.Result);

            var res8 = await Playback.GetTimePosition();
            Assert.True(res8.Succeeded);
            Assert.True(0 <= res8.Result);

            var res9 = await Playback.Seek(res8.Result + 2000);
            Assert.True(res9.Succeeded);
            Assert.True(res9.Result);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res10 = await Playback.GetTimePosition();
            Assert.True(res10.Succeeded);
            Assert.True((res8.Result + 2000) < res10.Result);
        }

        [Fact]
        public async Task GetCurrentTest()
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

            var res4 = await Playback.GetState();
            Assert.True(res4.Succeeded);

            if (res4.Result != Playback.PlaybackState.Stopped)
            {
                var res5 = await Playback.Stop();
                Assert.True(res5);
            }

            await Task.Delay(PlaybackTest.WaitMsec);

            var res6 = await Playback.Play(res3.Result[4].TlId);
            Assert.True(res6);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res7 = await Playback.GetCurrentTlTrack();
            Assert.True(res7.Succeeded);
            Assert.Equal(res7.Result.TlId, res3.Result[4].TlId);
            Assert.Equal(res7.Result.Track.Uri, res3.Result[4].Track.Uri);

            var res8 = await Playback.GetCurrentTrack();
            Assert.True(res8.Succeeded);
            Assert.Equal(res8.Result.Uri, res3.Result[4].Track.Uri);

            //var res9 = await Playback.GetStreamTitle();
            //Assert.True(res9.Succeeded);
            //Assert.Equal(res3.Result[4].Track.Name, res9.Result);
        }

        [Fact]
        public async Task SetStateTest()
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

            var res4 = await Playback.GetState();
            Assert.True(res4.Succeeded);

            if (res4.Result != Playback.PlaybackState.Stopped)
            {
                var res5 = await Playback.Stop();
                Assert.True(res5);
            }

            await Task.Delay(PlaybackTest.WaitMsec);

            var res6 = await Playback.Play(res3.Result[4].TlId);
            Assert.True(res6);

            await Task.Delay(PlaybackTest.WaitMsec);

            var res7 = await Playback.SetState(Playback.PlaybackState.Paused);
            Assert.True(res7);

            var res8 = await Playback.GetState();
            Assert.True(res8.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res8.Result);

            var res9 = await Playback.SetState(Playback.PlaybackState.Stopped);
            Assert.True(res9);

            var res10 = await Playback.GetState();
            Assert.True(res10.Succeeded);
            Assert.Equal(Playback.PlaybackState.Stopped, res10.Result);

            var res11 = await Playback.SetState(Playback.PlaybackState.Playing);
            Assert.True(res11);

            var res12 = await Playback.GetState();
            Assert.True(res12.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res12.Result);

            var res13 = await Playback.Stop();
            Assert.True(res13);
        }
    }
}
