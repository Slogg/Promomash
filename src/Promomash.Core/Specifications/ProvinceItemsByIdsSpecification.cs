using Ardalis.Specification;
using Promomash.Core.Entities;

namespace Promomash.Core.Specifications;

public class ProvinceItemsByIdsSpecification : Specification<Province>, ISingleResultSpecification
{
    public ProvinceItemsByIdsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }
}
