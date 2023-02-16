using FluentValidation;
using Promomash.Core.Interfaces;
using Promomash.Web.Models.Requests;

namespace Promomash.Web.Models.Validators;

public class UserRegisterModelValidator : AbstractValidator<UserRegisterRequest>
{
    public UserRegisterModelValidator(IUserService userService, ICountryService countryService, IProvinceService provinceService)
    {
        RuleFor(user => user).MustAsync(async (user, ct) =>
            {
                var result = await userService.IsExistAsync(user.Email, ct);
                return !result;
            })
            .WithMessage("This email already registered");

        RuleFor(user => user.Email)
            .NotNull()
            .EmailAddress()
            .WithMessage("Login must be a valid email");

        RuleFor(user => user.Password)
            .NotNull()
            .Must(x => x.Any(char.IsUpper))
            .Must(x => x.Any(char.IsDigit))
            .WithMessage("Password must contain min 1 digit and min 1 uppercase letter");

        RuleFor(user => user.IsAgree)
            .Must(x => x)
            .WithMessage("You must agree with rules");

        RuleFor(user => user.CountryId).MustAsync(async (id, ct) =>
            {
                var result = await countryService.IsExistsAsync(id, ct);
                return result;
            })
            .WithMessage("Country is a required field");

        RuleFor(user => user).MustAsync(async (user, ct) =>
            {
                var result = await provinceService.IsExistsAsync(user.ProvinceId, ct);
                return result;
            })
            .WithMessage("Province is a required field");
    }
}
