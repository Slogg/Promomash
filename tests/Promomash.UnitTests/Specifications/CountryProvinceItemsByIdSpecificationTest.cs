using Promomash.Core.Entities;
using Promomash.Core.Specifications;
using Xunit;

namespace Promomash.UnitTests.Specifications;

public class CountryProvinceItemsByIdSpecificationTest
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 4)]
    [InlineData(3, 2)]
    public void ProvinceCollection_NumberOfItems_Equal(int countryId, int expectedCount)
    {
        // Arrange
        var spec = new CountryProvinceItemsByIdSpecification(countryId);

        // Act
        var result = spec.Evaluate(getTestItemCollection()).FirstOrDefault();

        // Assert
        Assert.Equal(expectedCount, result.Provinces.Count());
    }

    private List<Country> getTestItemCollection()
    {
        return new List<Country>()
        {
            new Country(1, "Test 1", new List<Province>
            {
                new Province(1, "Test 1.1")
            }),
            new Country(2, "Test 2", new List<Province>
            {
                new Province(2, "Test 2.1"),
                new Province(3, "Test 2.2"),
                new Province(4, "Test 2.3"),
                new Province(5, "Test 2.4")
            }),
            new Country(3, "Test 3", new List<Province>
            {
                new Province(6, "Test 3.1"),
                new Province(7, "Test 3.2")
            })
        };
    }
}
