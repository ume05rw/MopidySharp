using Mopidy.Core;
using MopidySharpTest.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.Core
{
    public class LibraryTest : TestBase
    {
        [Fact]
        public async Task BrowseTest()
        {
            var res1 = await Library.Browse(null);
            Assert.True(res1.Succeeded);
            var result1 = res1.Result;

            foreach (var resRef in result1)
            {
                var res2 = await Library.Browse(resRef.Uri);
                Assert.True(res2.Succeeded);
            }

            var res3 = await Library.Browse("local:track?albumartist='Various Artists'");
        }

        [Fact]
        public async Task SearchByClassTest()
        {
            var query1 = new Library.Query();
            query1.Artist.Add("drum");
            var res1 = await Library.Search(query1);
            Assert.True(res1.Succeeded);
            var result = res1.Result;
        }
    }
}
