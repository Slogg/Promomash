using Ardalis.Specification;
using Promomash.Core.Entities;

namespace Promomash.Core.Specifications;

public class CountryProvinceItemsByIdSpecification : Specification<Country>, ISingleResultSpecification
{
    public CountryProvinceItemsByIdSpecification(int id)
    {
        Query.Where(c => c.Id == id).Include(c => c.Provinces.OrderBy(c => c.Name));
    }
}
