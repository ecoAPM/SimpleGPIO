using System;
using System.ComponentModel;
using System.Threading.Tasks;
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

        private readonly IPinInterface _enabled;
        private readonly IPinInterface _clockwise;
        private readonly IPinInterface _counterclockwise;

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
                    _counterclockwise?.TurnOff();
                    break;
                case Rotation.Counterclockwise:
                    _clockwise.TurnOff();
                    _counterclockwise?.TurnOn();
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(Direction));
            }
            _enabled?.TurnOn();
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

        public async Task RunFor(TimeSpan length, bool coast = false)
        {
            Start();
            await Task.Delay(length);

            if (coast)
                Coast();
            else
                Stop();
        }

        public async Task TurnClockwiseFor(TimeSpan length, bool coast = false)
        {
            Direction = Rotation.Clockwise;
            await RunFor(length, coast);
        }

        public async Task TurnCounterclockwiseFor(TimeSpan length, bool coast = false)
        {
            Direction = Rotation.Counterclockwise;
            await RunFor(length, coast);
        }

        public void Stop()
        {
            _clockwise.TurnOff();
            _counterclockwise?.TurnOff();
            _enabled?.TurnOff();
        }

        public void Coast() => _enabled?.TurnOff();
    }
}
