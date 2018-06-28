using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SimpleGPIO.OS
{
    public class FileSystem : IFileSystem
    {
        private readonly Func<string, IFileInfoWrapper> _newFileInfo;
        private readonly Func<IFileSystem, string, Func<bool>, Action, IFileWatcher> _newFileWatcher;

        public FileSystem(Func<string, IFileInfoWrapper> fileInfoFac, Func<IFileSystem, string, Func<bool>, Action, IFileWatcher> fileWatcherFac)
        {
            _newFileInfo = fileInfoFac;
            _newFileWatcher = fileWatcherFac;
        }

        public string Read(string path)
            => File.ReadAllText(path).Trim('\n');

        public void Write(string path, string content)
            => File.WriteAllText(path, content.Trim('\n'));

        public bool Exists(string path)
            => Directory.Exists(path) || File.Exists(path);

        public void WaitFor(string path, TimeSpan timeout)
            => WaitFor(path, false, timeout);

        public void WaitForWriteable(string path, TimeSpan timeout)
            => WaitFor(path, true, timeout);

        private readonly IDictionary<string, IFileWatcher> watchers = new Dictionary<string, IFileWatcher>();
        public void Watch(string path, Func<bool> predicate, Action action)
        {
            var watcher = watchers.ContainsKey(path)
                ? watchers[path]
                : watchers[path] = _newFileWatcher(this, path, predicate, action);
         
            if (action != null)
                watcher.Watch();
            else
                watcher.Stop();
        }

        private void WaitFor(string path, bool writeable, TimeSpan timeout)
        {
            var time = Stopwatch.StartNew();
            var fileInfo = _newFileInfo(path);

            while ((!fileInfo.Exists || writeable && fileInfo.IsReadOnly) && time.Elapsed < timeout)
            {
                fileInfo.Refresh();
                Thread.Sleep(1);
            }

            time.Stop();
            if (time.Elapsed > timeout)
                throw new TimeoutException();
        }
    }
}