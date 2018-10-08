using FluentValidation;
using System.IO;
using WpfGui.Models;

namespace WpfGui.Validators
{
    public class FileDataSourceValidator : AbstractValidator<FileDataSource>
    {
        public FileDataSourceValidator()
        {
            RuleFor(model => model.FilePath).Must(File.Exists).WithMessage("File does not exist");
        }
    }
}