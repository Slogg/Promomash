using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Promomash.Web.Models.Requests;
using Promomash.Core.Interfaces;

namespace Promomash.Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IValidator<UserRegisterRequest> _validator;
    private readonly IUserService _userService;

    public UserController(IValidator<UserRegisterRequest> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    public async Task<IActionResult> Register(UserRegisterRequest registerModel, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(registerModel, cancellationToken);
        if (!result.IsValid)
        {
            return BadRequest(result);
        }

        await _userService.AddAsync(registerModel.Email, registerModel.Password, registerModel.ProvinceId, cancellationToken);

        return Ok();
    }
}

