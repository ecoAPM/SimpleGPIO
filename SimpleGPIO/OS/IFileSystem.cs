using System;

namespace SimpleGPIO.OS
{
    public interface IFileSystem
    {
        string Read(string path);
        void Write(string path, string content);
        bool Exists(string path);
        void WaitFor(string path, TimeSpan timeout);
        void WaitForWriteable(string path, TimeSpan timeout);
        void Watch(string path, Func<bool> predicate, Action action);
    }
}