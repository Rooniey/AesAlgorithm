namespace WpfGui.Services
{
    public interface IFileService
    {
        void SaveFile(byte[] dataToSave, string fileName);
    }
}
