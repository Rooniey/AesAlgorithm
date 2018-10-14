using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Caliburn.Micro;
using Cryptography;
using Cryptography.Constants;
using Cryptography.Data.Sources;
using Cryptography.Utils;
using Microsoft.Win32;
using WpfGui.CustomFramework;
using WpfGui.Models;
using WpfGui.Services;
using WpfGui.Validators;
using static Cryptography.Utils.TextUtility;

namespace WpfGui.ViewModels
{
    public class AesFormViewModel : BindableBase
    {
        private ILog _logger = LogManager.GetLog(typeof(AesFormViewModel));
        private readonly ISymmetricCryptoService _cryptoService;
        private readonly IFileService _fileService;

        public enum Mode
        {
            Encrypt, 
            Decrypt
        }

        public AesFormViewModel(ISymmetricCryptoService cryptoService, IFileService fileService)
        {
            _logger.Info("Initialized AesFormViewModel");
            _cryptoService = cryptoService;
            _fileService = fileService;
            Cipherkey = new AesCipherkeyCreate(new AesCipherkeyValidator());
            TextSource = new TextSource(new TextDataSourceValidator());
            FileDataSource = new FileSource(new FileDataSourceValidator());
        }

        #region EXPOSED MODELS

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

        #endregion

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
            byte[] encryptedBytes = _cryptoService.Encrypt(GetKey(), GetDataSource(Mode.Encrypt));
            RedirectOutput(encryptedBytes, Mode.Encrypt);
        }

        public void Decrypt()
        {
            byte[] decryptedBytes = _cryptoService.Decrypt(GetKey(), GetDataSource(Mode.Decrypt));
            RedirectOutput(decryptedBytes, Mode.Decrypt);
        }

        private void RedirectOutput(byte[] data, Mode mode)
        {
            if (IsFileEncryption)
            {
                string filePath;
                if (mode == Mode.Encrypt)
                {
                    filePath = Regex.Replace(FileDataSource.FilePath, @"\..*$", "") + "-encrypted";
                }
                else
                {
                    filePath = Regex.Replace(FileDataSource.FilePath, @"\..*$", "") + "-decrypted";
                }
                _fileService.SaveFile(data, filePath);
            }
            else
            {
                if (mode == Mode.Encrypt)
                {
                    EncryptedString = BitConverter.ToString(data).Replace("-", "");
                }
                else
                {
                    DecryptedString = data.ToText(TextSource.Encoding);
                }
            }

        }

        private IDataSource GetDataSource(Mode mode)
        {
            if (IsFileEncryption)
            {
                return new FileDataSource(FileDataSource.FilePath);
            }

            if (mode == Mode.Encrypt)
            {
                return new TextDataSource(TextSource.Text, TextSource.Encoding);
            }

            return new MemoryDataSource(Enumerable.Range(0, EncryptedString.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(EncryptedString.Substring(x, 2), 16))
                    .ToArray());
                         
        }

        private byte[] GetKey()
        {
            return Cipherkey.Cipherkey.ToByteArray(Cipherkey.SelectedEncoding);
        }
    }
}