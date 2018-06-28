using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleGPIO.OS
{
    public class FileWatcher : IFileWatcher
    {
        private readonly string _path;
        private readonly Func<bool> _predicate;
        private readonly Action _action;
        
        public bool IsRunning { get; private set; }
        private readonly IFileSystem _fs;

        public FileWatcher(IFileSystem fs, string path, Func<bool> predicate, Action action)
        {
            _path = path;
            _predicate = predicate;
            _action = action;

            _fs = fs;
        }

        public void Watch()
        {
            Task.Run(() => RunWatch());
        }

        private void RunWatch()
        {
            IsRunning = true;
            string lastValue = null;
            while (IsRunning)
            {
                var newValue = _fs.Read(_path);
                if (newValue != lastValue && _predicate())
                    _action();
                Thread.Sleep(1);
                lastValue = newValue;
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
