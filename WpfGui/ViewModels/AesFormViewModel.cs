using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using AesAlgorithm;
using AesAlgorithm.Utils;
using Cryptography;
using Cryptography.Constants;
using Cryptography.Data.Sources;
using WpfGui.CustomFramework;
using WpfGui.Models;
using WpfGui.Validators;
using static AesAlgorithm.Utils.TextUtility;
using FileSource = WpfGui.Models.FileSource;

namespace WpfGui.ViewModels
{
    public class AesFormViewModel : BindableBase
    {
        private ILog _logger = LogManager.GetLog(typeof(AesFormViewModel));
        public ISymmetricCryptoService CryptoService { get; private set; }

        public AesFormViewModel(ISymmetricCryptoService cryptoService)
        {
            _logger.Info("Initialized AesFormViewModel");
            CryptoService = cryptoService;
            Cipherkey = new AesCipherkeyCreate(new AesCipherkeyValidator());
            TextSource = new TextSource(new TextDataSourceValidator());
            FileDataSource = new FileSource(new FileDataSourceValidator());
        }

        private AesCipherkeyCreate _cipherkey;

        public AesCipherkeyCreate Cipherkey
        {
            get => _cipherkey;
            set => SetProperty(ref _cipherkey, value);
        }

        private TextSource _textSource;

        public TextSource TextSource
        {
            get => _textSource;
            set => SetProperty(ref _textSource, value);
        }

        private FileSource _fileDataSource;

        public FileSource FileDataSource
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

        private string _decryptedString;

        public string DecryptedString
        {
            get => _decryptedString;
            set => SetProperty(ref _decryptedString, value);
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

        public bool CanEncrypt()
        {
            if (Cipherkey.HasErrors) return false;
            if (IsFileEncryption)
            {
                return !FileDataSource.HasErrors;
            }
            else
            {
                return !TextSource.HasErrors;
            }

        }

        public void Encrypt()
        {
            byte[] key = Cipherkey.Cipherkey.ToByteArray(Cipherkey.SelectedEncoding);

            if (IsFileEncryption)
            {
               
            }
            else
            {
                IDataSource dataSource = new TextDataSource() {Text = TextSource.Text, TextEncoding = TextSource.Encoding};
                byte[] encryptedBytes = CryptoService.Encrypt(key, dataSource);
                EncryptedString = encryptedBytes.ToText(TextSource.Encoding);
            }
        }
    }
}