using Ardalis.Specification;
using Promomash.Core.Entities;

namespace Promomash.Core.Specifications;

public class UserByNameItemsSpecification : Specification<User>, ISingleResultSpecification
{
    public UserByNameItemsSpecification(string email)
    {
        Query.Where(item => email == item.Email);
    }
}
