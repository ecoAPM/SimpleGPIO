using System;
using System.Diagnostics;
using System.Threading;
using SimpleGPIO.IO;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO
{
    public abstract class PinInterface : IPinInterface
    {
        public abstract bool Enabled { get; set; }

        public IOMode IOMode
        {
            get => Direction.ToIOMode();
            set => Direction = value.ToDirection();
        }

        public abstract Direction Direction { get; set; }

        private IPowerMode _powerMode = SimpleGPIO.Power.PowerMode.Direct;
        public IPowerMode PowerMode
        {
            get => _powerMode;
            set
            {
                _powerMode = value;
                TurnOff();
            }
        }

        public PowerValue Power
        {
            get => Voltage.ToPowerValue(PowerMode);
            set => Voltage = value == PowerValue.On ? PowerMode.On : PowerMode.Off;
        }

        public abstract Voltage Voltage { get; set; }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public void TurnOn() => Power = PowerValue.On;
        public void TurnOff() => Power = PowerValue.Off;

        public void Spike()
        {
            TurnOn();
            TurnOff();
        }

        public void TurnOnFor(TimeSpan length)
        {
            TurnOn();
            Thread.Sleep(length);
            TurnOff();
        }

        public void TurnOffFor(TimeSpan length)
        {
            TurnOff();
            Thread.Sleep(length);
            TurnOn();
        }

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

        private static long Delay(double hz) => (long) (TimeSpan.TicksPerSecond / hz / 2);

        public void Toggle(double hz, ulong iterations)
        {
            var delay = Delay(hz);
            var stopwatch = Stopwatch.StartNew();
            ulong run = 0;
            while (run++ < iterations)
                RunToggleIteration(stopwatch, delay);
        }

        public abstract void OnPowerOn(Action action);
        public abstract void OnPowerOff(Action action);
        public abstract void OnPowerChange(Action action);

        public void Dispose() => Disable();
    }
}