using FluentValidation;

namespace Application.Features.BookFeatures.Commands.Create
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(request => request.Author)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage(x => "{PropertyName} is required, please input it!");
        }
    }
}