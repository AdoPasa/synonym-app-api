using Application.Synonyms.Models;
using FluentValidation;

namespace WebApi.Validators
{
    public class AddSynonymRequestValidator : AbstractValidator<AddSynonymRequest>
    {
        public AddSynonymRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("error.synonym.validation.name-required")
                .MaximumLength(90)
                .WithMessage("error.synonym.validation.name-invalid-length");

            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("error.synonym.validation.description-required")
                .MaximumLength(300)
                .WithMessage("error.synonym.validation.description-invalid-length");
        }
    }
}
