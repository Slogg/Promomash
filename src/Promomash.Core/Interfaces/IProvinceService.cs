using Promomash.Core.Entities;

namespace Promomash.Core.Interfaces;

public interface IProvinceService
{
    public Task<IReadOnlyList<Province>> GetProvincesByCountryIdAsync(int countryId, CancellationToken cancellationToken);
    
    public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken);
}