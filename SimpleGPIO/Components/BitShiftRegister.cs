using SimpleGPIO.GPIO;
using SimpleGPIO.Power;

namespace SimpleGPIO.Components
{
    public class BitShiftRegister
    {
        private readonly IPinInterface _enabled;
        private readonly IPinInterface _data;
        private readonly IPinInterface _shift;
        private readonly IPinInterface _output;
        private readonly IPinInterface _clear;

        public BitShiftRegister(IPinInterface enabledPin, IPinInterface dataPin, IPinInterface shiftPin, IPinInterface outputPin, IPinInterface clearPin = null)
        {
            _enabled = enabledPin;
            _data = dataPin;
            _shift = shiftPin;
            _output = outputPin;
            _clear = clearPin;

            if(_enabled != null)
                _enabled.PowerMode = PowerMode.Differential;
            
            if (_clear != null)
                _clear.PowerMode = PowerMode.Differential;
        }

        public void SetValue(byte value)
        {
            var a = GetBinaryDigitPowerValue(value, 7);
            var b = GetBinaryDigitPowerValue(value, 6);
            var c = GetBinaryDigitPowerValue(value, 5);
            var d = GetBinaryDigitPowerValue(value, 4);
            var e = GetBinaryDigitPowerValue(value, 3);
            var f = GetBinaryDigitPowerValue(value, 2);
            var g = GetBinaryDigitPowerValue(value, 1);
            var h = GetBinaryDigitPowerValue(value, 0);
            SetPowerValues(a, b, c, d, e, f, g, h);
        }

        public static PowerValue GetBinaryDigitPowerValue(byte value, byte digit)
            => ((value >> digit) & 1) == 1
                ? PowerValue.On
                : PowerValue.Off;

        public void SetPowerValues(PowerValue A, PowerValue B, PowerValue C, PowerValue D, PowerValue E, PowerValue F, PowerValue G, PowerValue H)
        {
            _enabled?.TurnOn();

            _data.Power = A;
            _shift.Spike();

            _data.Power = B;
            _shift.Spike();

            _data.Power = C;
            _shift.Spike();

            _data.Power = D;
            _shift.Spike();

            _data.Power = E;
            _shift.Spike();

            _data.Power = F;
            _shift.Spike();

            _data.Power = G;
            _shift.Spike();

            _data.Power = H;
            _shift.Spike();

            _output.Spike();
        }

        public void Clear()
        {
            _clear?.TurnOn();
            _output.Spike();
            _clear?.TurnOff();
        }
    }
}