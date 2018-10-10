using FluentValidation;
using FluentValidation.Internal;
using WpfGui.CustomFramework;

namespace WpfGui.Models
{
    public class FileDataSource : ValidatableBindableBase<FileDataSource>
    {
        public FileDataSource(AbstractValidator<FileDataSource> validator) : base(validator)
        {
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        protected override ValidationContext<FileDataSource> ProvideContext(string propertyName)
        {
            return new ValidationContext<FileDataSource>(this,
                new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
        }
    }
}