namespace Promomash.Core.Interfaces;

public interface IUserService
{
    public Task<bool> IsExistAsync(string email, CancellationToken cancellationToken);
    
    public Task AddAsync(string email, string password, int provinceId, CancellationToken cancellationToken);
}