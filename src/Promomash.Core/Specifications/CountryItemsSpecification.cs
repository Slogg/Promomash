using Ardalis.Specification;
using Promomash.Core.Entities;

namespace Promomash.Core.Specifications;

public class CountryItemsSpecification : Specification<Country>, ISingleResultSpecification
{
    public CountryItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }

    public CountryItemsSpecification()
    {
        Query.OrderBy(c => c.Name);
    }
}
