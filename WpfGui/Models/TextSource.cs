using Cryptography.Utils;
using FluentValidation;
using FluentValidation.Internal;
using WpfGui.CustomFramework;

namespace WpfGui.Models
{
    public class TextSource : ValidatableBindableBase<TextSource>
    {
        public TextSource(AbstractValidator<TextSource> validator) : base(validator)
        {
        }

        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private TextUtility.Encoding _encoding;

        public TextUtility.Encoding Encoding
        {
            get => _encoding;
            set => SetProperty(ref _encoding, value);
        }

        protected override ValidationContext<TextSource> ProvideContext(string propertyName)
        {
            return new ValidationContext<TextSource>(this,
                new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
        }
    }
}