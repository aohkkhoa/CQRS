using Application.Features.BookFeatures.Commands.Create;
using FluentValidation;

namespace Application.Validators.Features.Books.Commands
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(request => request.CreateBookViewModel.Author)
                .Must(x => !string.IsNullOrWhiteSpace(x)).OverridePropertyName(nameof(CreateBookViewModel.Author))
                .WithMessage(x => "{PropertyName} is required, please input it!");
        }
    }
}