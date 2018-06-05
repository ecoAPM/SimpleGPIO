namespace SimpleGPIO.OS
{
    public interface IFileSystem
    {
        string Read(string path);
        void Write(string path, string content);
        bool Exists(string path);
    }
}