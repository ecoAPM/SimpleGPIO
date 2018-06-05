using System.IO;
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
    }
}
