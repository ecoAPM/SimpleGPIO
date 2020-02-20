using System;
using SimpleGPIO.IO;
using SimpleGPIO.OS;
using SimpleGPIO.Power;

namespace SimpleGPIO.GPIO
{
    public class LinuxPinInterface : PinInterface
    {
        private readonly byte _pin;
        private readonly IFileSystem _fs;

        public LinuxPinInterface(byte bcmIdentifier, IFileSystem fileSystem)
        {
            _pin = bcmIdentifier;
            _fs = fileSystem;
        }

        private const string BaseDir = "/sys/class/gpio/";
        private string PinDir => BaseDir + "gpio" + _pin;
        private string DirectionPath => PinDir + "/direction";
        private string VoltagePath => PinDir + "/value";

        private bool? enabled;
        public override bool Enabled
        {
            get => (bool)(enabled ?? (enabled = _fs.Exists(PinDir)));
            set
            {
                enabled = value;
                _fs.Write(BaseDir + (value ? "export" : "unexport"), _pin.ToString());
            }
        }

        private Direction? direction;
        public override Direction Direction
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

        private Voltage? voltage;
        public override Voltage Voltage
        {
            get
            {
                if (!Enabled)
                    Enable();

                if(voltage == null)
                    _fs.WaitFor(VoltagePath, TimeSpan.FromSeconds(1));

                return Direction == Direction.In
                    ? (Voltage)(voltage = getVoltageFromFileSystem())
                    : (Voltage)(voltage ?? (voltage = getVoltageFromFileSystem()));
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

        private Voltage getVoltageFromFileSystem()
        {
            return (Voltage)byte.Parse(_fs.Read(VoltagePath));
        }

        public override void OnPowerOn(Action action)
        {
            if (!Enabled)
                Enable();

            _fs.Watch(VoltagePath, () => Power == PowerValue.On, action);
        }

        public override void OnPowerOff(Action action)
        {
            if (!Enabled)
                Enable();
            
            _fs.Watch(VoltagePath, () => Power == PowerValue.Off, action);
        }

        public override void OnPowerChange(Action action)
        {
            if (!Enabled)
                Enable();
            
            _fs.Watch(VoltagePath, () => true, action);
        }
    }
}