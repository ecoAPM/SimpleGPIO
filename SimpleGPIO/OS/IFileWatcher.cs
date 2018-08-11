using System;

namespace SimpleGPIO.OS
{
    public interface IFileWatcher : IDisposable
    {
        void Watch();
        void Stop();
    }
}
