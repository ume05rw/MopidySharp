using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mopidy.Core;
using MopidySharpTest.Bases;
using System.Threading.Tasks;

namespace MopidySharpTest.Core
{
    [TestClass]
    public class HistoryTest : TestBase
    {
        [TestMethod]
        public async Task GetHistoryTest()
        {
            var result = await History.GetHistory();
            Assert.IsTrue(result.Succeeded);
            Assert.IsNotNull(result.Result);
            Assert.IsTrue(0 <= result.Result.Count);
        }
    }
}
