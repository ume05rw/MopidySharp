using Mopidy.Core;
using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class PlaybackTest : TestBase
    {
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

            if (res4.State == Playback.PlaybackState.Playing)
            {
                var res5 = await Playback.Stop();
                Assert.True(res5);
            }

            var res6 = await Playback.Play(res3.Result[3].TlId);
            Assert.True(res6);

            var res7 = await Playback.GetState();
            Assert.True(res7.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res7.State);

            var res8 = await Playback.Stop();
            Assert.True(res8);

            var res9 = await Playback.Play();
            Assert.True(res9);

            var res10 = await Playback.GetState();
            Assert.True(res10.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res10.State);

            var res11 = await Playback.Next();
            Assert.True(res11);

            var res12 = await Playback.GetState();
            Assert.True(res12.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res12.State);

            var res13 = await Playback.Previous();
            Assert.True(res13);

            var res14 = await Playback.GetState();
            Assert.True(res14.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res14.State);

            var res15 = await Playback.Pause();
            Assert.True(res15);

            var res16 = await Playback.GetState();
            Assert.True(res16.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res16.State);

            var res17 = await Playback.Next();
            Assert.True(res17);

            var res18 = await Playback.GetState();
            Assert.True(res18.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res18.State);

            var res19 = await Playback.Previous();
            Assert.True(res19);

            var res20 = await Playback.GetState();
            Assert.True(res20.Succeeded);
            Assert.Equal(Playback.PlaybackState.Paused, res20.State);

            var res21 = await Playback.Resume();
            Assert.True(res21);

            var res22 = await Playback.GetState();
            Assert.True(res22.Succeeded);
            Assert.Equal(Playback.PlaybackState.Playing, res22.State);

            var res23 = await Playback.Stop();
            Assert.True(res21);

            var res24 = await Playback.GetState();
            Assert.True(res24.Succeeded);
            Assert.Equal(Playback.PlaybackState.Stopped, res24.State);
        }
    }
}
