using FindService.EF;
using FluentValidation;

namespace FindService.Services.Validators
{
    public class AdvertisementDtoRequestValidator : AbstractValidator<AdvertisementDtoRequest>
    {
        public AdvertisementDtoRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
