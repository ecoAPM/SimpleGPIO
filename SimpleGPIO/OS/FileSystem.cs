using System.IO;

namespace SimpleGPIO.OS
{
    public class FileSystem : IFileSystem
    {
        public string Read(string path)
            => File.ReadAllText(path).Trim('\n');

        public void Write(string path, string content)
            => File.WriteAllText(path, content.Trim('\n'));

        public bool Exists(string path)
            => Directory.Exists(path) || File.Exists(path);
    }
}