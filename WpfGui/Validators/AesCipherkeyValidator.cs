using FluentValidation;
using WpfGui.Models;

namespace WpfGui.Validators
{
    internal class AesCipherkeyValidator : AbstractValidator<AesCipherkeyCreate>
    {
        public AesCipherkeyValidator()
        {
            RuleFor(model => model.CurrentLength)
                .Must((model, length) => length == model.CipherkeyLength)
                .WithMessage("Provide key with correct number of bits");
        }
    }
}