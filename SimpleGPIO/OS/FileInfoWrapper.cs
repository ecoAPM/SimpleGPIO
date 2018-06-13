using System.IO;

namespace SimpleGPIO.OS
{
    public class FileInfoWrapper : IFileInfoWrapper
    {
        private readonly FileInfo _fileInfo;
        public FileInfoWrapper(string path) => _fileInfo = new FileInfo(path);
        public bool Exists => _fileInfo.Exists;
        public bool IsReadOnly => _fileInfo.IsReadOnly;
        public void Refresh() => _fileInfo.Refresh();
    }
}