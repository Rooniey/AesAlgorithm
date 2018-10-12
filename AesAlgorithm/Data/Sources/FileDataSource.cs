using System;
using System.IO;

namespace Cryptography.Data.Sources
{
    public class FileDataSource : IDataSource
    {
        public string FilePath { get; set; }

        public FileDataSource(string filePath)
        {
            FilePath = filePath;
        }

        public byte[] GetData()
        {
            if (!File.Exists(FilePath))
            {
                throw new ArgumentException($"FileDataSource: can not set {FilePath} for data source; file does not exist");
            }

            return File.ReadAllBytes(FilePath);
        }
    }
}