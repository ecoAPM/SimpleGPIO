using System;
using System.Runtime.InteropServices;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Boards
{
    public static class PinInterfaceFactory
    {
        public static IPinInterface NewPinInterface(byte bcmIdentifier)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new LinuxPinInterface(bcmIdentifier);

            throw new NotImplementedException($"{GetOSName()} is not yet supported");
        }

        private static string GetOSName()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows"
            : (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "OSX"
            : "Unknown OS");
    }
}