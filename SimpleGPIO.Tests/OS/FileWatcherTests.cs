using System.Threading.Tasks;
using NSubstitute;
using SimpleGPIO.OS;
using Xunit;

namespace SimpleGPIO.Tests.OS
{
    public class FileWatcherTests
    {
        [Fact]
        public async Task WatchStartsWatcher()
        {
            //arrange
            var fs = Substitute.For<IFileSystem>();
            using (var watcher = new FileWatcher(fs, "", () => true, () => { }))
            {
                //act
                watcher.Watch(0);
                await Task.Delay(1);

                //assert
                Assert.True(watcher.IsRunning);
            }
        }

        [Fact]
        public async Task StopStopsWatcher()
        {
            //arrange
            var fs = Substitute.For<IFileSystem>();
            using (var watcher = new FileWatcher(fs, "", () => true, () => { }))
            {
                watcher.Watch(0);
                await Task.Delay(1);

                //act
                watcher.Stop();
                await Task.Delay(1);

                //assert
                Assert.False(watcher.IsRunning);
            }
        }
    }
}