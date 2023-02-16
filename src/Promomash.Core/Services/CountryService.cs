using Promomash.Core.Interfaces;
using Promomash.Core.Entities;
using Promomash.Core.Specifications;

namespace Promomash.Core.Services;

public sealed class CountryService : ICountryService
{
    private readonly IReadRepository<Country> _countryRepository;

    public CountryService(IReadRepository<Country> countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IReadOnlyList<Country>> GetCountriesAsync(CancellationToken cancellationToken)
    {
        var countrySortedSpec = new CountryItemsSpecification();
        return await _countryRepository.ListAsync(countrySortedSpec, cancellationToken);
    }

    public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken)
    {
        var countryByIdSpec = new CountryItemsSpecification(id);
        return _countryRepository.AnyAsync(countryByIdSpec, cancellationToken);
    }
}
