using Mopidy.Core;
using MopidySharpTest.Bases;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class HistoryTest : TestBase
    {
        [Fact]
        public async Task GetHistoryTest()
        {
            var res = await History.GetHistory();
            Assert.True(res.Succeeded);
            Assert.NotNull(res.Result);
            Assert.True(0 <= res.Result.Count);
        }

        [Fact]
        public async Task GetLengthTest()
        {
            var res = await History.GetLength();
            Assert.True(res.Succeeded);
            Assert.True(0 <= res.Result);
        }
    }
}
