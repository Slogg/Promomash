using Promomash.Core.Interfaces;
using Promomash.Core.Entities;
using Promomash.Core.Specifications;

namespace Promomash.Core.Services;

public sealed class ProvinceService : IProvinceService
{
    private readonly IRepository<Province> _provinceRepository;
    private readonly IRepository<Country> _countryRepository;

    public ProvinceService(IRepository<Province> provinceRepository, IRepository<Country> countryRepository)
    {
        _provinceRepository = provinceRepository;
        _countryRepository = countryRepository;
    }

    public async Task<IReadOnlyList<Province>> GetProvincesByCountryIdAsync(int countryId, CancellationToken cancellationToken)
    {
        var countryProvinceSortedSpec = new CountryProvinceItemsByIdSpecification(countryId);
        var coutryWithProvince = await _countryRepository.FirstOrDefaultAsync(countryProvinceSortedSpec, cancellationToken);
        return coutryWithProvince.Provinces.ToList();
    }

    public Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken)
    {
        var provinceByIdSpec = new ProvinceItemsByIdsSpecification(id);
        return _provinceRepository.AnyAsync(provinceByIdSpec, cancellationToken);
    }
}
