﻿using Mopidy.Core;
using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class PlaylistsTest : TestBase
    {
        [Fact]
        public async Task GetUriSchemesTest()
        {
            var res1 = await Playlists.GetUriSchemes();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
        }

        [Fact]
        public async Task AsListTest()
        {
            var res1 = await Playlists.AsList();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
        }

        [Fact]
        public async Task GetItemsTest()
        {
            var res1 = await Playlists.AsList();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);

            foreach (var plRef in res1.Result)
            {
                var res2 = await Playlists.GetItems(plRef.Uri);
                Assert.True(res2.Succeeded);
                Assert.True(0 <= res2.Result.Length);
            }
        }

        [Fact]
        public async Task LookupTest()
        {
            var res1 = await Playlists.AsList();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);

            foreach (var plRef in res1.Result)
            {
                var res2 = await Playlists.Lookup(plRef.Uri);
                Assert.True(res2.Succeeded);
                Assert.NotNull(res2.Result);
                Assert.True(0 <= res2.Result.Tracks.Count);
            }
        }


        [Fact]
        public async Task RefreshTest()
        {
            var res1 = await Playlists.GetUriSchemes();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);

            foreach (var scheme in res1.Result)
            {
                var res2 = await Playlists.Refresh(scheme);
                Assert.True(res2);
            }

            var res3 = await Playlists.Refresh();
            Assert.True(res3);
        }

        [Fact]
        public async Task CreateSaveDeleteTest()
        {
            var res1 = await Playlists.Create("tmp_playlist");
            Assert.True(res1.Succeeded);

            var list = res1.Result;

            var res2 = await Library.Search(
                queryArtist: "Air Supply",
                queryAlbum: "Strangers In Love"
            );
            Assert.True(res2.Succeeded);
            Assert.True(1 <= res2.Result.Length);
            list.Tracks.AddRange(res2.Result.First().Tracks);

            var res3 = await Playlists.Save(list);
            Assert.True(res3.Succeeded);

            var res4 = await Playlists.Lookup(list.Uri);
            Assert.True(res4.Succeeded);
            Assert.Equal(list.Tracks.Count, res4.Result.Tracks.Count);

            for (var i = 0; i < list.Tracks.Count; i++)
            {
                Assert.Equal(list.Tracks[i].Uri, res4.Result.Tracks[i].Uri);
            }

            var res5 = await Playlists.Delete(list.Uri);
            Assert.True(res5.Succeeded);
            Assert.True(res5.Result);

            var res6 = await Playlists.Lookup(list.Uri);
            Assert.True(res6.Succeeded);
            Assert.Null(res6.Result);
        }
    }
}
