using System;

namespace SimpleGPIO.OS
{
    public interface IFileWatcher : IDisposable
    {
        void Watch(int pollInterval = 1);
        void Stop();
    }
}
