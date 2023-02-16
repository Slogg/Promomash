using Promomash.Core.Entities;
using Promomash.Core.Interfaces;
using Promomash.Core.Specifications;

namespace Promomash.Core.Services;

public sealed class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsExistAsync(string email, CancellationToken cancellationToken)
    {
        var userByNameItemsSpec = new UserByNameItemsSpecification(email);
        return await _userRepository.AnyAsync(userByNameItemsSpec, cancellationToken);
    }

    public async Task AddAsync(string email, string password, int provinceId, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), email, BCrypt.Net.BCrypt.HashPassword(password), provinceId);

        await _userRepository.AddAsync(user, cancellationToken);
    }
}
