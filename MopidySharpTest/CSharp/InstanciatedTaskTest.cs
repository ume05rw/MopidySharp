using System;
using System.Threading.Tasks;
using Xunit;

namespace MopidySharpTest.CSharp
{
    public class InstanciatedTaskTest
    {
        [Fact]
        public async Task TaskTest()
        {
            var isExecuted = false;

            var task = new Task<bool>(() =>
            {
                isExecuted = true;

                return true;
            });

            var waitMsec = 3000;
            _ = Task.Delay(waitMsec)
                .ContinueWith(t => {
                    task.Start();
                });

            Assert.False(isExecuted);

            var now = DateTime.Now;

            await task;

            var span = DateTime.Now - now;
            Assert.True(waitMsec <= span.TotalMilliseconds);
            Assert.True(isExecuted);
            Assert.True(task.Result);
        }
    }
}
