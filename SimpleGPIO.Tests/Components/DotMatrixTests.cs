using System.Collections.Generic;
using SimpleGPIO.Components;
using SimpleGPIO.Power;
using SimpleGPIO.Tests.GPIO;
using Xunit;

namespace SimpleGPIO.Tests.Components
{
    public class DotMatrixTests
    {
        private static DotMatrix.PinSet StubPinSet(IReadOnlyList<StubPinInterface> pins)
        {
            return new DotMatrix.PinSet
            {
                Pin1 = pins[1],
                Pin2 = pins[2],
                Pin3 = pins[3],
                Pin4 = pins[4],
                Pin5 = pins[5],
                Pin6 = pins[6],
                Pin7 = pins[7],
                Pin8 = pins[8],

                Pin9 = pins[9],
                Pin10 = pins[10],
                Pin11 = pins[11],
                Pin12 = pins[12],
                Pin13 = pins[13],
                Pin14 = pins[14],
                Pin15 = pins[15],
                Pin16 = pins[16]
            };
        }

        private static StubPinInterface[] StubPins()
        {
            const byte numPins = 16;
            var pins = new StubPinInterface[numPins + 1];
            for (byte x = 1; x <= numPins; x++)
                pins[x] = new StubPinInterface(x);
            
            return pins;
        }

        [Fact]
        public void RowsAndColumnsAreSetFromInput()
        {
            //arrange
            var pins = StubPins();
            var pinSet = StubPinSet(pins);

            //act
            var matrix = new DotMatrix(pinSet);
            
            //assert
            Assert.Equal(pins[9], matrix.Row[0]);
            Assert.Equal(pins[14], matrix.Row[1]);
            Assert.Equal(pins[8], matrix.Row[2]);
            Assert.Equal(pins[12], matrix.Row[3]);
            Assert.Equal(pins[1], matrix.Row[4]);
            Assert.Equal(pins[7], matrix.Row[5]);
            Assert.Equal(pins[2], matrix.Row[6]);
            Assert.Equal(pins[5], matrix.Row[7]);
            
            Assert.Equal(pins[13], matrix.Column[0]);
            Assert.Equal(pins[3], matrix.Column[1]);
            Assert.Equal(pins[4], matrix.Column[2]);
            Assert.Equal(pins[10], matrix.Column[3]);
            Assert.Equal(pins[6], matrix.Column[4]);
            Assert.Equal(pins[11], matrix.Column[5]);
            Assert.Equal(pins[15], matrix.Column[6]);
            Assert.Equal(pins[16], matrix.Column[7]);
        }

        [Fact]
        public void CanTurnOnAllRows()
        {
            //arrange
            var pins = StubPins();
            var pinSet = StubPinSet(pins);
            var matrix = new DotMatrix(pinSet);

            //act
            matrix.SetAllRows(PowerValue.On);
            
            //assert
            for(var x = 0; x < 8; x++)
                Assert.Equal(PowerValue.On, matrix.Row[x].Power);
        }

        [Fact]
        public void CanTurnOnAllColumns()
        {
            //arrange
            var pins = StubPins();
            var pinSet = StubPinSet(pins);
            var matrix = new DotMatrix(pinSet);

            //act
            matrix.SetAllColumns(PowerValue.On);
            
            //assert
            for(var x = 0; x < 8; x++)
                Assert.Equal(PowerValue.On, matrix.Column[x].Power);
        }

        [Fact]
        public void CanTurnOnAllPins()
        {
            //arrange
            var pins = StubPins();
            var pinSet = StubPinSet(pins);
            var matrix = new DotMatrix(pinSet);

            //act
            matrix.SetAll(PowerValue.On);
            
            //assert
            for (var x = 0; x < 8; x++)
            {
                Assert.Equal(PowerValue.On, matrix.Row[x].Power);
                Assert.Equal(PowerValue.On, matrix.Column[x].Power);
            }
        }
    }
}