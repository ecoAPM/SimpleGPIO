using System;
using SimpleGPIO.Components;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components
{
    public class MotorTests
    {
        [Fact]
        public void StartWhenClockwiseSetsInputsCorrectly()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise)
            {
                Direction = Motor.Rotation.Clockwise
            };

            //act
            motor.Start();

            //assert
            Assert.Equal(PowerValue.On, enabled.Power);
            Assert.Equal(PowerValue.On, clockwise.Power);
            Assert.Equal(PowerValue.Off, counterclockwise.Power);
        }

        [Fact]
        public void StartWhenCounterclockwiseSetsInputsCorrectly()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise)
            {
                Direction = Motor.Rotation.Counterclockwise
            };

            //act
            motor.Start();

            //assert
            Assert.Equal(PowerValue.On, enabled.Power);
            Assert.Equal(PowerValue.Off, clockwise.Power);
            Assert.Equal(PowerValue.On, counterclockwise.Power);
        }

        [Fact]
        public void TurnClockwiseSetsDirection()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise)
            {
                Direction = Motor.Rotation.Counterclockwise
            };

            //act
            motor.TurnClockwise();

            //assert
            Assert.Equal(Motor.Rotation.Clockwise, motor.Direction);
        }

        [Fact]
        public void TurnCounterclockwiseSetsDirection()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise)
            {
                Direction = Motor.Rotation.Clockwise
            };

            //act
            motor.TurnCounterclockwise();

            //assert
            Assert.Equal(Motor.Rotation.Counterclockwise, motor.Direction);
        }

        [Fact]
        public void RunForStopsWhenDone()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);

            //act
            motor.RunFor(TimeSpan.Zero);

            //assert
            Assert.Equal(PowerValue.Off, clockwise.Power);
            Assert.Equal(PowerValue.Off, counterclockwise.Power);
        }

        [Fact]
        public void RunForCanCoastWhenDone()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);

            //act
            motor.RunFor(TimeSpan.Zero, true);

            //assert
            Assert.Equal(PowerValue.Off, enabled.Power);
            Assert.Equal(PowerValue.On, clockwise.Power);
            Assert.Equal(PowerValue.Off, counterclockwise.Power);
        }

        [Fact]
        public void TurnClockwiseForSetsDirection()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);

            //act
            motor.TurnClockwiseFor(TimeSpan.Zero);

            //assert
            Assert.Equal(Motor.Rotation.Clockwise, motor.Direction);
        }

        [Fact]
        public void TurnCounterclockwiseForSetsDirection()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);

            //act
            motor.TurnCounterclockwiseFor(TimeSpan.Zero);

            //assert
            Assert.Equal(Motor.Rotation.Counterclockwise, motor.Direction);
        }

        [Fact]
        public void StopTurnsOffAllPins()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);
            motor.Start();

            //act
            motor.Stop();

            //assert
            Assert.Equal(PowerValue.Off, enabled.Power);
            Assert.Equal(PowerValue.Off, clockwise.Power);
            Assert.Equal(PowerValue.Off, counterclockwise.Power);
        }

        [Fact]
        public void CoastKeepsInputsOn()
        {
            //arrange
            var enabled = new StubPinInterface(1);
            var clockwise = new StubPinInterface(2);
            var counterclockwise = new StubPinInterface(3);
            var motor = new Motor(enabled, clockwise, counterclockwise);
            motor.Start();

            //act
            motor.Coast();

            //assert
            Assert.Equal(PowerValue.Off, enabled.Power);
            Assert.Equal(PowerValue.On, clockwise.Power);
            Assert.Equal(PowerValue.Off, counterclockwise.Power);
        }
    }
}
