using Promomash.Core.Entities;

namespace Promomash.Core.Interfaces;

public interface ICountryService
{
    public Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken);

    public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken);
}
