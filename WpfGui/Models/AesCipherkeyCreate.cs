using Cryptography.Utils;
using FluentValidation;
using FluentValidation.Internal;
using WpfGui.CustomFramework;

namespace WpfGui.Models
{
    public class AesCipherkeyCreate : ValidatableBindableBase<AesCipherkeyCreate>
    {
        public AesCipherkeyCreate(AbstractValidator<AesCipherkeyCreate> validator) : base(validator)
        {
        }

        private string _cipherkey = "";

        public string Cipherkey
        {
            get => _cipherkey;
            set
            {
                SetProperty(ref _cipherkey, value);
                UpdateCurrentLength();
            }
        }

        private int _cipherkeyLength;

        public int CipherkeyLength
        {
            get => _cipherkeyLength;
            set => SetProperty(ref _cipherkeyLength, value);
        }

        private int _currentLength;

        public int CurrentLength
        {
            get => _currentLength;
            set => SetProperty(ref _currentLength, value);
        }

        private TextUtility.Encoding _selectedEncoding;

        public TextUtility.Encoding SelectedEncoding
        {
            get => _selectedEncoding;
            set
            {
                SetProperty(ref _selectedEncoding, value);
                UpdateCurrentLength();
            }
        }

        private void UpdateCurrentLength()
        {
            CurrentLength = Cipherkey.ToByteArray(SelectedEncoding).Length * 8;
        }

        protected override ValidationContext<AesCipherkeyCreate> ProvideContext(string propertyName)
        {
            return new ValidationContext<AesCipherkeyCreate>(this,
                new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
        }
    }
}