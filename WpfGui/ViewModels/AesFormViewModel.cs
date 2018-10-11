using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AesAlgorithm;
using AesAlgorithm.Constants;
using AesAlgorithm.Data.Processors;
using AesAlgorithm.Utils;
using WpfGui.CustomFramework;
using WpfGui.Models;
using WpfGui.Validators;
using static AesAlgorithm.Utils.TextUtility;

namespace WpfGui.ViewModels
{
    public class AesFormViewModel : BindableBase
    {
        private ILog _logger = LogManager.GetLog(typeof(AesFormViewModel));

        public AesFormViewModel()
        {
            _logger.Info("Initialized AesFormViewModel");
            Cipherkey = new AesCipherkeyCreate(new AesCipherkeyValidator());
            TextDataSource = new TextDataSource(new TextDataSourceValidator());
            FileDataSource = new FileDataSource(new FileDataSourceValidator());
        }

        private AesCipherkeyCreate _cipherkey;

        public AesCipherkeyCreate Cipherkey
        {
            get => _cipherkey;
            set => SetProperty(ref _cipherkey, value);
        }

        private TextDataSource _textDataSource;

        public TextDataSource TextDataSource
        {
            get => _textDataSource;
            set => SetProperty(ref _textDataSource, value);
        }

        private FileDataSource _fileDataSource;

        public FileDataSource FileDataSource
        {
            get => _fileDataSource;
            set => SetProperty(ref _fileDataSource, value);
        }

        private bool _isFileEncryption;

        public bool IsFileEncryption
        {
            get => _isFileEncryption;
            set => SetProperty(ref _isFileEncryption, value);
        }

        private string _encryptedString;

        public string EncryptedString
        {
            get => _encryptedString;
            set => SetProperty(ref _encryptedString, value);
        }


        public List<int> KeyLengths => AesParameters.KEY_LENGTHS.ToList();
        public List<Encoding> Encodings => Enum.GetValues(typeof(Encoding)).OfType<Encoding>().ToList();

        public void ChooseFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a file to encrypt";

            if (fileDialog.ShowDialog() == true)
            {
                FileDataSource.FilePath = fileDialog.FileName;
            }
        }

        public void Encrypt()
        {
            if (!IsFileEncryption)
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(TextDataSource.Text);
                AesDataProcessor processor = new AesDataProcessor();
                byte[,] key =
                    MatrixOperations.ConvertToKeyMatrix(TextUtility.ToByteArray(Cipherkey.Cipherkey,
                        Cipherkey.SelectedEncoding));
                AesAlgorithmImp algorithm = new AesAlgorithmImp(key);
                List<byte[,]> blocks = processor.ConvertToBlocks(data);
                var encryptedBlocks = algorithm.Encrypt(blocks);
                var encBytes = processor.ConvertToByteArray(encryptedBlocks);
                EncryptedString = System.Text.Encoding.ASCII.GetString(encBytes);
                Console.WriteLine("kupa");


            }
        }
    }
}