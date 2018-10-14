using System.Linq;
using Cryptography.Constants;
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
            RuleFor(model => model.CipherkeyLength)
                .Must(chosenLength => AesParameters.KEY_LENGTHS.Contains(chosenLength))
                .WithMessage("Chosen length should be of size 128, 192, 256 bits length");
        }
    }
}