using System;
using System.Collections.Generic;
using System.Text;
using SimpleGPIO.Power;
using Xunit;

namespace SimpleGPIO.Tests.Power
{
    public class PowerHelpersTests
    {
        [Theory]
        [InlineData(typeof(Direct), Voltage.Low, PowerValue.Off)]
        [InlineData(typeof(Direct), Voltage.High, PowerValue.On)]
        [InlineData(typeof(Differential), Voltage.Low, PowerValue.On)]
        [InlineData(typeof(Differential), Voltage.High, PowerValue.Off)]
        public void CanConvertPowerToVoltage(Type powerModeType, Voltage voltage, PowerValue expected)
        {
            //arrange
            var powerMode = (IPowerMode)Activator.CreateInstance(powerModeType);

            //act
            var power = voltage.ToPowerValue(powerMode);

            //assert
            Assert.Equal(expected, power);
        }
    }
}
