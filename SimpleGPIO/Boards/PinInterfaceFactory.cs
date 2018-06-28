using System;
using System.Runtime.InteropServices;
using SimpleGPIO.GPIO;
using SimpleGPIO.OS;

namespace SimpleGPIO.Boards
{
    public static class PinInterfaceFactory
    {
        public static IPinInterface NewPinInterface(byte bcmIdentifier)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return new LinuxPinInterface(bcmIdentifier, FileSystem);

            throw new NotImplementedException($"{GetOSName()} is not yet supported");
        }

        private static FileSystem FileSystem
            => new FileSystem(path => new FileInfoWrapper(path), (fs, path, predicate, action) => new FileWatcher(fs, path, predicate, action));

        private static string GetOSName()
            => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Windows"
            : (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "OSX"
            : "Unknown OS");
    }
}