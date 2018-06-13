using System;
using System.IO;
using System.Threading.Tasks;
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
            var fs = new FileSystem();

            //act
            var data = fs.Read("readme.txt");

            //assert
            Assert.Equal("123", data);
        }

        [Fact]
        public void WriteTrimsBlankLines()
        {
            //arrange
            var fs = new FileSystem();

            //act
            fs.Write("writeme.txt", "456\n");

            //assert
            Assert.Equal("456", File.ReadAllText("writeme.txt"));
        }

        [Fact]
        public void ExistsWhenDirectoryExists()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var exists = fs.Exists(Directory.GetCurrentDirectory());

            //assert
            Assert.True(exists);
        }

        [Fact]
        public void ExistsWhenFileExists()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var exists = fs.Exists("readme.txt");

            //assert
            Assert.True(exists);
        }

        [Fact]
        public void DoesNotExistWhenNeitherDirectoryNorFileExists()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var exists = fs.Exists("other");

            //assert
            Assert.False(exists);
        }

        [Fact]
        public async Task WaitForPassesWhenFileExists()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var wait = fs.WaitFor("readme.txt", TimeSpan.FromMilliseconds(1));

            //assert
            await wait;
        }

        [Fact]
        public async Task WaitForFailsWhenFileDoesNotExist()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var wait = fs.WaitFor("other", TimeSpan.FromMilliseconds(1));

            //assert
            await Assert.ThrowsAsync<TimeoutException>(() => wait);
        }

        [Fact]
        public async Task WaitForWriteablePassesWhenFileIsWriteable()
        {
            //arrange
            var fs = new FileSystem();

            //act
            var wait = fs.WaitForWriteable("readme.txt", TimeSpan.FromMilliseconds(1));

            //assert
            await wait;
        }

        [Fact]
        public async Task WaitForWriteableFailsWhenFileIsReadOnly()
        {
            //arrange
            var fileInfo = Substitute.For<IFileInfoWrapper>();
            fileInfo.IsReadOnly.Returns(true);
            var fs = new FileSystem(path => fileInfo);

            //act
            var wait = fs.WaitForWriteable("readonly.txt", TimeSpan.FromMilliseconds(1));

            //assert
            await Assert.ThrowsAsync<TimeoutException>(() => wait);
        }
    }
}
