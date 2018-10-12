using FluentValidation;
using WpfGui.Models;

namespace WpfGui.Validators
{
    public class TextDataSourceValidator : AbstractValidator<TextSource>
    {
        public TextDataSourceValidator()
        {
            RuleFor(model => model.Text).NotNull().NotEmpty().WithMessage("Text data source can not be empty");
        }
    }
}