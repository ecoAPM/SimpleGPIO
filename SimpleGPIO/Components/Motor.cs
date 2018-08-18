using System;
using System.ComponentModel;
using System.Threading;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Components
{
    public class Motor
    {
        public enum Rotation
        {
            Clockwise,
            Counterclockwise
        }

        private readonly IPinInterface _clockwise;
        private readonly IPinInterface _counterclockwise;
        private readonly IPinInterface _enabled;

        public Rotation Direction { get; set; } = Rotation.Clockwise;

        public Motor(IPinInterface enabledPin, IPinInterface clockwisePin, IPinInterface counterclockwisePin = null)
        {
            _enabled = enabledPin;
            _clockwise = clockwisePin;
            _counterclockwise = counterclockwisePin;
        }

        public void Start()
        {
            switch (Direction)
            {
                case Rotation.Clockwise:
                    _clockwise.TurnOn();
                    _counterclockwise.TurnOff();
                    break;
                case Rotation.Counterclockwise:
                    _clockwise.TurnOff();
                    _counterclockwise.TurnOn();
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(Direction));
            }
            _enabled.TurnOn();
        }

        public void TurnClockwise()
        {
            Direction = Rotation.Clockwise;
            Start();
        }

        public void TurnCounterclockwise()
        {
            Direction = Rotation.Counterclockwise;
            Start();
        }

        public void RunFor(TimeSpan length, bool coast = false)
        {
            Start();
            Thread.Sleep(length);

            if (coast)
                Coast();
            else
                Stop();
        }

        public void TurnClockwiseFor(TimeSpan length, bool coast = false)
        {
            Direction = Rotation.Clockwise;
            RunFor(length, coast);
        }

        public void TurnCounterclockwiseFor(TimeSpan length, bool coast = false)
        {
            Direction = Rotation.Counterclockwise;
            RunFor(length, coast);
        }

        public void Stop()
        {
            _clockwise.TurnOff();
            _counterclockwise.TurnOff();
            _enabled.TurnOff();
        }

        public void Coast()
        {
            _enabled.TurnOff();
        }
    }
}
