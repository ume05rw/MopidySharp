using System;
using System.Threading.Tasks;
using Xunit;
using Mopidy.Core;
using MopidySharpTest.Bases;

namespace MopidySharp.Test
{
    public class HistoryTest : TestBase
    {
        [Fact]
        public async Task GetHistoryTest()
        {
            var result = await History.GetHistory();
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Result);
            Assert.True(0 <= result.Result.Count);
        }
    }
}
