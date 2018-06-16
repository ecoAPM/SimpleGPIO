using System;
using System.Collections.Generic;
using System.Text;
using SimpleGPIO.Power;
using Xunit;

namespace SimpleGPIO.Tests.Power
{
    public class PowerModeTests
    {
        [Fact]
        public void CanCreateDirect()
        {
            //act
            var powerMode = PowerMode.Direct;

            //assert
            Assert.IsAssignableFrom<Direct>(powerMode);
        }

        [Fact]
        public void CanCreateDifferential()
        {
            //act
            var powerMode = PowerMode.Differential;

            //assert
            Assert.IsAssignableFrom<Differential>(powerMode);
        }
    }
}
