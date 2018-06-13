using System;
using System.IO;
using SimpleGPIO.OS;
using Xunit;

namespace SimpleGPIO.Tests.OS
{
    public class FileInfoWrapperTests
    {
        [Fact]
        public void CanCheckIfFileExists()
        {
            //arrange
            var info = new FileInfoWrapper("readme.txt");

            //act
            var exists = info.Exists;

            //assert
            Assert.True(exists);
        }

        [Fact]
        public void CanCheckIfFileIsReadOnly()
        {
            //arrange
            var info = new FileInfoWrapper("readme.txt");

            //act
            var readOnly = info.IsReadOnly;

            //assert
            Assert.False(readOnly);
        }
    }
}
