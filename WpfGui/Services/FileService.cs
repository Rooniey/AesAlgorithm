using System.IO;

namespace WpfGui.Services
{
    public class FileService : IFileService
    {
        public void SaveFile(byte[] dataToSave, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                fs.Write(dataToSave, 0, dataToSave.Length);
            }
        }
    }
}
