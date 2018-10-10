using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<int> KeyLengths => AesAlgorithm.AesAlgorithm.KEY_SIZES.ToList();
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
    }
}