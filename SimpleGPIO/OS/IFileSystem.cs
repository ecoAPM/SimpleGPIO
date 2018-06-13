using System;
using System.Threading.Tasks;

namespace SimpleGPIO.OS
{
    public interface IFileSystem
    {
        string Read(string path);
        void Write(string path, string content);
        bool Exists(string path);
        Task WaitFor(string path, TimeSpan timeout);
        Task WaitForWriteable(string path, TimeSpan timeout);
    }
}