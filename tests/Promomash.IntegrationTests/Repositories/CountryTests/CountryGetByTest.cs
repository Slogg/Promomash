using Microsoft.EntityFrameworkCore;
using Promomash.Core.Entities;
using Promomash.Infrastructure;
using Xunit;

namespace Promomash.IntegrationTests.Repositories.CountryTests;

public class CountryGetByTest
{
    private readonly AppDbContext _catalogContext;
    private readonly EfRepository<Country> _countryRepository;
    public CountryGetByTest()
    {
        var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
        _catalogContext = new AppDbContext(dbOptions);
        _countryRepository = new EfRepository<Country>(_catalogContext);
    }

    [Fact]
    public async Task EfRepostiryCountry_GetsExistingProvince_Equal()
    {
        // Arrange
        var provinceName = "Test Province";
        var countryName = "Test Country";
        var country = new Country(1, countryName, new List<Province>
        {
           new Province(1, provinceName)
        });
        _catalogContext.Countries.Add(country);
        _catalogContext.SaveChanges();

        // Act
        var countryFromRepo = await _countryRepository.GetByIdAsync(country.Id);
        var firstItem = countryFromRepo.Provinces.FirstOrDefault();

        // Assert
        Assert.Equal(country.Id, countryFromRepo.Id);
        Assert.Equal(provinceName, firstItem.Name);
    }
}
