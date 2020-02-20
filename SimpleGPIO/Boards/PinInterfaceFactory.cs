using System;
using System.Device.Gpio;
using System.Runtime.InteropServices;
using SimpleGPIO.GPIO;
using SimpleGPIO.OS;

namespace SimpleGPIO.Boards
{
    public static class PinInterfaceFactory
    {
        public static IPinInterface NewPinInterface(byte bcmIdentifier) => NewPinInterface(bcmIdentifier, false);

        public static IPinInterface NewPinInterface(byte bcmIdentifier, bool experimental)
        {
            if(experimental)
                return new SystemPinInterface(bcmIdentifier, GpioController);
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new LinuxPinInterface(bcmIdentifier, FileSystem);

            throw new NotImplementedException($"{GetOSName()} is not yet supported");
        }

        private static GpioControllerWrapper GpioController => new GpioControllerWrapper(PinNumberingScheme.Logical);

        private static FileSystem FileSystem
            => new FileSystem(path => new FileInfoWrapper(path), (fs, path, predicate, action) => new FileWatcher(fs, path, predicate, action));

        private static string GetOSName()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows"
            : (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "OSX"
            : "Unknown OS");
    }
}