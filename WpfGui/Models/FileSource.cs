using FluentValidation;
using FluentValidation.Internal;
using WpfGui.CustomFramework;

namespace WpfGui.Models
{
    public class FileSource : ValidatableBindableBase<FileSource>
    {
        public FileSource(AbstractValidator<FileSource> validator) : base(validator)
        {
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        protected override ValidationContext<FileSource> ProvideContext(string propertyName)
        {
            return new ValidationContext<FileSource>(this,
                new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
        }
    }
}