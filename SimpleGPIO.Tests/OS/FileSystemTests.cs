using System;
using System.IO;
using NSubstitute;
using SimpleGPIO.OS;
using Xunit;

namespace SimpleGPIO.Tests.OS
{
    public class FileSystemTests
    {
        [Fact]
        public void ReadTrimsBlankLines()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            var data = fs.Read("readme.txt");

            //assert
            Assert.Equal("123", data);
        }

        [Fact]
        public void WriteTrimsBlankLines()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            fs.Write("writeme.txt", "456\n");

            //assert
            Assert.Equal("456", File.ReadAllText("writeme.txt"));
        }

        [Fact]
        public void ExistsWhenDirectoryExists()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            var exists = fs.Exists(Directory.GetCurrentDirectory());

            //assert
            Assert.True(exists);
        }

        [Fact]
        public void ExistsWhenFileExists()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            var exists = fs.Exists("readme.txt");

            //assert
            Assert.True(exists);
        }

        [Fact]
        public void DoesNotExistWhenNeitherDirectoryNorFileExists()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            var exists = fs.Exists("other");

            //assert
            Assert.False(exists);
        }

        [Fact]
        public void WaitForPassesWhenFileExists()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            fileInfo.Exists.Returns(true);
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            fs.WaitFor("readme.txt", TimeSpan.FromMilliseconds(20), 0);

            //assert
            Assert.True(true);
        }

        [Fact]
        public void WaitForFailsWhenFileDoesNotExist()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            var wait = new Action(() => fs.WaitFor("other", TimeSpan.FromMilliseconds(20), 0));

            //assert
            Assert.Throws<TimeoutException>(wait);
        }

        [Fact]
        public void WaitForWriteablePassesWhenFileIsWriteable()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            fileInfo.Exists.Returns(true);
            fileInfo.IsReadOnly.Returns(false);
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);

            //act
            fs.WaitForWriteable("readme.txt", TimeSpan.FromMilliseconds(20), 0);

            //assert
            Assert.True(true);
        }

        [Fact]
        public void WaitForWriteableFailsWhenFileIsReadOnly()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            fileInfo.IsReadOnly.Returns(true);
            var nfw = Substitute.For<IFileWatcher>();
            var fs = new FileSystem(path => fileInfo, (fileSystem, path, predicate, action) => nfw);

            //act
            var wait = new Action(() => fs.WaitForWriteable("readonly.txt", TimeSpan.FromMilliseconds(20), 0));

            //assert
            Assert.Throws<TimeoutException>(wait);
        }

        [Fact]
        public void WatchStartsWatcherWhenActionIsNotNull()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<IFileWatcher>();
            var fs = new FileSystem(path => fileInfo, (fileSystem, path, predicate, action) => nfw);

            //act
            fs.Watch("", () => true, () => { });

            //assert
            nfw.Received().Watch();
        }

        [Fact]
        public void WatchStopsWatcherWhenActionIsNull()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<IFileWatcher>();
            var fs = new FileSystem(path => fileInfo, (fileSystem, path, predicate, action) => nfw);

            //act
            fs.Watch("", () => true, null);

            //assert
            nfw.Received().Stop();
        }

        [Fact]
        public void WatchUsesCachedWatcherWhenExists()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            var nfw = Substitute.For<Func<IFileSystem, string, Func<bool>, Action, IFileWatcher>>();
            var fs = new FileSystem(path => fileInfo, nfw);
            fs.Watch("", () => true, () => { });

            //act
            fs.Watch("", () => true, () => { });

            //assert
            nfw.Received(1).Invoke(Arg.Any<IFileSystem>(), Arg.Any<string>(), Arg.Any<Func<bool>>(), Arg.Any<Action>());
        }
    }
}
