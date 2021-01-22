using Mopidy.Core;
using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class MixerTest : TestBase
    {
        [Fact]
        public async Task GetMuteTest()
        {
            var res = await Mixer.GetMute();
            Assert.True(res.Succeeded);
        }

        [Fact]
        public async Task SetMuteTest()
        {
            var res1 = await Mixer.GetMute();
            Assert.True(res1.Succeeded);

            var res2 = await Mixer.SetMute(!res1.Result);
            Assert.True(res2.Succeeded);
            Assert.True(res2.Result);

            var res3 = await Mixer.GetMute();
            Assert.True(res3.Succeeded);
            Assert.True(res1.Result != res3.Result);
        }

        [Fact]
        public async Task GetVolumeTest()
        {
            var res = await Mixer.GetVolume();
            Assert.True(res.Succeeded);
            if (res.Result != null)
            {
                var volume = (int)res.Result;
                Assert.InRange<int>(volume, 0, 100);
            }
        }

        [Fact]
        public async Task SetVolumeTest()
        {
            var res1 = await Mixer.SetVolume(100);
            Assert.True(res1.Succeeded);
            Assert.True(res1.Result);

            var res2 = await Mixer.GetVolume();
            Assert.True(res2.Succeeded);
            Assert.Equal(100, res2.Result);

            var res3 = await Mixer.SetVolume(0);
            Assert.True(res3.Succeeded);
            Assert.True(res3.Result);

            var res4 = await Mixer.GetVolume();
            Assert.True(res4.Succeeded);
            Assert.Equal(0, res4.Result);

            var res5 = await Mixer.SetVolume(50);
            Assert.True(res5.Succeeded);
            Assert.True(res5.Result);

            var res6 = await Mixer.GetVolume();
            Assert.True(res6.Succeeded);
            Assert.Equal(50, res6.Result);
        }
    }
}
