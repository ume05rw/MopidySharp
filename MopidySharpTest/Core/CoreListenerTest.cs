using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Mopidy.Core;
using MopidySharpTest.Bases;
using System.Linq;

namespace MopidySharpTest.Core
{

    public class CoreListenerTest : TestBase
    {
        private const int WaitMsec = 500;

        [Fact]
        public async Task MuteChangedTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

            var res1 = await Mixer.GetMute();
            Assert.True(res1.Succeeded);

            var recieved = false;
            var changed = false;
            CoreListener.MuteChanged += (sender, ev) =>
            {
                recieved = true;

                if (ev.Mute != res1.Result)
                    changed = true;
            };

            var res2 = await Mixer.SetMute(!res1.Result);
            Assert.True(res2.Succeeded);
            Assert.True(res2.Result);
            await Task.Delay(WaitMsec);

            Assert.True(recieved);
            Assert.True(changed);
        }

        [Fact]
        public async Task OptionsChangedTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

            var res1 = await Tracklist.GetConsume();
            Assert.True(res1.Succeeded);

            var res2 = await Tracklist.GetRandom();
            Assert.True(res2.Succeeded);

            var res3 = await Tracklist.GetRepeat();
            Assert.True(res3.Succeeded);

            var res4 = await Tracklist.GetSingle();
            Assert.True(res4.Succeeded);

            var recieved = false;

            CoreListener.OptionsChanged += (sender, ev) =>
            {
                recieved = true;
            };

            var res12 = await Tracklist.SetConsume(!res1.Result);
            Assert.True(res12);
            await Task.Delay(WaitMsec);
            Assert.True(recieved);

            recieved = false;
            var res22 = await Tracklist.SetRandom(!res2.Result);
            Assert.True(res22);
            await Task.Delay(WaitMsec);
            Assert.True(recieved);

            recieved = false;
            var res32 = await Tracklist.SetRepeat(!res3.Result);
            Assert.True(res32);
            await Task.Delay(WaitMsec);
            Assert.True(recieved);

            recieved = false;
            var res42 = await Tracklist.SetSingle(!res4.Result);
            Assert.True(res42);
            await Task.Delay(WaitMsec);
            Assert.True(recieved);
        }

        [Fact]
        public async Task PlaybackStateChangedTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

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

            var beforeState = res4.Result;
            var recieved = false;
            var matchedOld = false;
            var changedNew = false;

            CoreListener.PlaybackStateChanged += (sender, ev) =>
            {
                recieved = true;
                if (beforeState == ev.OldState)
                    matchedOld = true;
                if (beforeState != ev.NewState)
                    changedNew = true;
            };

            if (res4.Result != Mopidy.Models.Enums.PlaybackState.Playing)
            {
                var res5 = await Playback.Play(res3.Result[4].TlId);
                Assert.True(res5);

                await Task.Delay(WaitMsec);
                Assert.True(recieved);
                Assert.True(matchedOld);
                Assert.True(changedNew);
            }


            beforeState = Mopidy.Models.Enums.PlaybackState.Playing;
            recieved = false;
            matchedOld = false;
            changedNew = false;
            var res6 = await Playback.Pause();
            Assert.True(res6);

            await Task.Delay(WaitMsec);
            Assert.True(recieved);
            Assert.True(matchedOld);
            Assert.True(changedNew);


            beforeState = Mopidy.Models.Enums.PlaybackState.Paused;
            recieved = false;
            matchedOld = false;
            changedNew = false;
            var res7 = await Playback.Stop();
            Assert.True(res7);

            await Task.Delay(WaitMsec);
            Assert.True(recieved);
            Assert.True(matchedOld);
            Assert.True(changedNew);


            beforeState = Mopidy.Models.Enums.PlaybackState.Stopped;
            recieved = false;
            matchedOld = false;
            changedNew = false;
            var res8 = await Playback.Play();
            Assert.True(res8);

            await Task.Delay(WaitMsec);
            Assert.True(recieved);
            Assert.True(matchedOld);
            Assert.True(changedNew);
        }

        [Fact]
        public async Task PlaylistEventTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

            var loaded = false;
            var changed = false;
            var deleted = false;

            CoreListener.PlaylistsLoaded += (sender, ev) =>
            {
                loaded = true;
            };
            CoreListener.PlaylistChanged += (sender, ev) =>
            {
                changed = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.Playlist);
                Assert.NotNull(ev.Playlist.Uri);
                Assert.NotNull(ev.Playlist.Tracks);
                Assert.True(0 <= ev.Playlist.Tracks.Count);
            };
            CoreListener.PlaylistDeleted += (sender, ev) =>
            {
                deleted = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.Uri);
            };

            changed = false;
            var res1 = await Playlists.Create(
                "tmp_playlist"
            );
            Assert.True(res1.Succeeded);
            var list = res1.Result;

            await Task.Delay(WaitMsec);
            Assert.True(changed);


            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            list.Tracks.AddRange(res2.Result.First().Tracks);


            changed = false;
            var res3 = await Playlists.Save(list);
            Assert.True(res3.Succeeded);
            await Task.Delay(WaitMsec);
            Assert.True(changed);


            deleted = false;
            var res4 = await Playlists.Delete(list.Uri);
            Assert.True(res4.Succeeded);
            Assert.True(res4.Result);
            await Task.Delay(WaitMsec);
            Assert.True(deleted);


            loaded = false;
            var res5 = await Playlists.Refresh();
            Assert.True(res5);
            await Task.Delay(WaitMsec);
            Assert.True(loaded);
        }

        [Fact]
        public async Task TracklistEventTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

            var seeked = false;
            var seekedCount = 0;
            var tracklistChanged = false;
            var trackPlaybackStarted = false;
            var trackPlaybackPaused = false;
            var trackPlaybackResumed = false;
            var trackPlaybackEnded = false;

            CoreListener.Seeked += (sender, ev) =>
            {
                seeked = true;
                seekedCount++;
                Assert.NotNull(ev);
                Assert.True(0 <= ev.TimePosition);
            };
            CoreListener.StreamTitleChanged += (sender, ev) =>
            {
                Assert.NotNull(ev);
                Assert.NotNull(ev.Title);
            };
            CoreListener.TracklistChanged += (sender, ev) =>
            {
                tracklistChanged = true;
                Assert.NotNull(ev);
            };
            CoreListener.TrackPlaybackStarted += (sender, ev) =>
            {
                trackPlaybackStarted = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.TlTrack);
                Assert.True(0 <= ev.TlTrack.TlId);
                Assert.NotNull(ev.TlTrack.Track);
            };
            CoreListener.TrackPlaybackPaused += (sender, ev) =>
            {
                trackPlaybackPaused = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.TlTrack);
                Assert.True(0 <= ev.TlTrack.TlId);
                Assert.NotNull(ev.TlTrack.Track);
                Assert.True(0 <= ev.TimePosition);
            };
            CoreListener.TrackPlaybackResumed += (sender, ev) =>
            {
                trackPlaybackResumed = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.TlTrack);
                Assert.True(0 <= ev.TlTrack.TlId);
                Assert.NotNull(ev.TlTrack.Track);
                Assert.True(0 <= ev.TimePosition);
            };
            CoreListener.TrackPlaybackEnded += (sender, ev) =>
            {
                trackPlaybackEnded = true;
                Assert.NotNull(ev);
                Assert.NotNull(ev.TlTrack);
                Assert.True(0 <= ev.TlTrack.TlId);
                Assert.NotNull(ev.TlTrack.Track);
                Assert.True(0 <= ev.TimePosition);
            };

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

            tracklistChanged = false;
            var res3 = await Tracklist.Add(uris);
            Assert.True(res3.Succeeded);
            await Task.Delay(WaitMsec);
            Assert.True(tracklistChanged);

            trackPlaybackStarted = false;
            var res4 = await Playback.Play(res3.Result[4].TlId);
            Assert.True(res4);
            await Task.Delay(WaitMsec);
            Assert.True(trackPlaybackStarted);

            trackPlaybackPaused = false;
            var res5 = await Playback.Pause();
            Assert.True(res5);
            await Task.Delay(WaitMsec);
            Assert.True(trackPlaybackPaused);

            trackPlaybackResumed = false;
            var res6 = await Playback.Resume();
            Assert.True(res5);
            await Task.Delay(WaitMsec);
            Assert.True(trackPlaybackResumed);

            seeked = false;
            trackPlaybackEnded = false;
            var res7 = await Playback.Seek((int)res3.Result[4].Track.Length - 1000);
            await Task.Delay((int)(WaitMsec * 2.1));
            Assert.True(seeked);
            Assert.True(trackPlaybackEnded);
            Assert.True(0 <= seekedCount);
        }

        [Fact]
        public async Task VolumeChangedTest()
        {
            if (Mopidy.Settings.ConnectionType != Mopidy.Settings.Connection.WebSocket)
                return;

            var res1 = await Mixer.GetVolume();
            Assert.True(res1.Succeeded);
            Assert.True(0 <= res1.Result);

            var beforeVolume = res1.Result;
            var recieved = false;
            var volumeChanged = false;
            CoreListener.VolumeChanged += (sender, ev) =>
            {
                recieved = true;

                Assert.NotNull(ev);
                Assert.True(0 <= ev.Volume);
                if (ev.Volume != beforeVolume)
                    volumeChanged = true;
            };

            if (res1.Result != 100)
            {
                var res2 = await Mixer.SetVolume(100);
                Assert.True(res2.Succeeded);
                Assert.True(res2.Result);
                await Task.Delay(WaitMsec);
                Assert.True(recieved);
                Assert.True(volumeChanged);
            }

            beforeVolume = 100;
            recieved = false;
            volumeChanged = false;
            var res3 = await Mixer.SetVolume(0);
            Assert.True(res3.Succeeded);
            Assert.True(res3.Result);
            await Task.Delay(WaitMsec);
            Assert.True(recieved);
            Assert.True(volumeChanged);
        }
    }
}
