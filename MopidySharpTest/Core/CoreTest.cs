using MopidySharpTest.Bases;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class CoreTest : TestBase
    {
        [Fact]
        public async Task GetUriSchemesTest()
        {
            var res1 = await Mopidy.Core.Core.GetUriSchemes();
            Assert.True(res1.Succeeded);
            Assert.True(0 < res1.Result.Length);
        }

        [Fact]
        public async Task GetVersionTest()
        {
            var res1 = await Mopidy.Core.Core.GetVersion();
            Assert.True(res1.Succeeded);
            Assert.NotNull(res1.Result);
        }
    }
}
