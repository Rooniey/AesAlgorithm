﻿using System;
using System.IO;

namespace Cryptography.Data.Sources
{
    public class FileDataSource : IDataSource
    {
        public string FilePath { get; }

        public FileDataSource(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("FileDataSource constructor: given file does not exist and can not be used as data source");
            }
            FilePath = filePath;
        }

        public byte[] GetData()
        {
            if (!File.Exists(FilePath))
            {
                throw new ArgumentException($"FileDataSource: file {FilePath} does not exist any more");
            }

            return File.ReadAllBytes(FilePath);
        }
    }
}