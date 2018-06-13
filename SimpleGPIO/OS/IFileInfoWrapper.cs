namespace SimpleGPIO.OS
{
    public interface IFileInfoWrapper
    {
        bool Exists { get; }
        bool IsReadOnly { get; }
        void Refresh();
    }
}