using System;
using System.Diagnostics;
using System.Threading;
using SimpleGPIO.IO;
using SimpleGPIO.OS;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO
{
    public class LinuxPinInterface : IPinInterface
    {
        public LinuxPinInterface(byte bcmIdentifier, IFileSystem fileSystem = null)
        {
            _pin = bcmIdentifier;
            _fs = fileSystem ?? new FileSystem();
        }

        private readonly byte _pin;
        private readonly IFileSystem _fs;

        private const string BaseDir = "/sys/class/gpio/";
        private string PinDir => BaseDir + "gpio" + _pin;
        private string DirectionPath => PinDir + "/direction";
        private string VoltagePath => PinDir + "/value";

        public IPowerMode PowerMode { get; set; } = SimpleGPIO.Power.PowerMode.Direct;

        private bool? enabled;
        public bool Enabled
        {
            get => (bool)(enabled ?? (enabled = _fs.Exists(PinDir)));
            set
            {
                enabled = value;
                _fs.Write(BaseDir + (value ? "export" : "unexport"), _pin.ToString());
            }
        }

        public IOMode IOMode
        {
            get => Direction.ToIOMode();
            set => Direction = value.ToDirection();
        }

        private Direction? direction;
        public Direction Direction
        {
            get
            {
                if (!Enabled)
                    Enable();

                if(direction == null)
                    _fs.WaitFor(DirectionPath, TimeSpan.FromSeconds(1));

                return (Direction)(direction ?? (direction = _fs.Read(DirectionPath).ToDirection()));
            }
            set
            {
                if (!Enabled)
                    Enable();

                direction = value;
                _fs.WaitForWriteable(DirectionPath, TimeSpan.FromSeconds(1));
                _fs.Write(DirectionPath, value.ToString().ToLower());
            }
        }

        public PowerValue Power
        {
            get => Voltage.ToPowerValue(PowerMode);
            set => Voltage = value == PowerValue.On ? PowerMode.On : PowerMode.Off;
        }

        private Voltage? voltage;
        public Voltage Voltage
        {
            get
            {
                if (!Enabled)
                    Enable();

                if(voltage == null)
                    _fs.WaitFor(VoltagePath, TimeSpan.FromSeconds(1));
                return (Voltage)(voltage ?? (voltage = (Voltage)byte.Parse(_fs.Read(VoltagePath))));
            }
            set
            {
                if (IOMode != IOMode.Write)
                    IOMode = IOMode.Write;

                voltage = value;
                _fs.WaitForWriteable(VoltagePath, TimeSpan.FromSeconds(1));
                _fs.Write(VoltagePath, ((byte)value).ToString());
            }
        }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public void TurnOn() => Power = PowerValue.On;
        public void TurnOff() => Power = PowerValue.Off;
        public void Toggle() => Power = Power == PowerValue.Off ? PowerValue.On : PowerValue.Off;

        public void Toggle(double hz, TimeSpan duration)
        {
            var delay = Delay(hz);
            var stopwatch = Stopwatch.StartNew();
            var expected = hz * duration.TotalSeconds;
            var count = 0;
            while (stopwatch.Elapsed.Ticks <= duration.Ticks && count++ < expected)
                RunToggleIteration(stopwatch, delay);
        }

        private void RunToggleIteration(Stopwatch stopwatch, long delay)
        {
            RunToggleHalfIteration(stopwatch, delay);
            RunToggleHalfIteration(stopwatch, delay);
        }

        private void RunToggleHalfIteration(Stopwatch stopwatch, long delay)
        {
            var start = stopwatch.Elapsed.Ticks;
            Toggle();
            var end = stopwatch.Elapsed.Ticks;
            var spent = end - start;
            Thread.Sleep(TimeSpan.FromTicks(spent < delay ? delay - spent : 1));
        }

        private static long Delay(double hz)
            => (long)(TimeSpan.TicksPerSecond / hz / 2);

        public void Toggle(double hz, ulong iterations)
        {
            var delay = Delay(hz);
            var stopwatch = Stopwatch.StartNew();
            ulong run = 0;
            while (run++ < iterations)
                RunToggleIteration(stopwatch, delay);
        }

        public void Dispose() => Disable();
    }
}