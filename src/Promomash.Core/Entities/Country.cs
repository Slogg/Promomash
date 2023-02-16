using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Promomash.Core.Interfaces;

namespace Promomash.Core.Entities;

[Table("Countries")]
public class Country : BaseEntity<int>, IAggregateRoot
{
    public Country() { }


    public Country(int id, string name, List<Province> provinces)
    {
        Id = id;
        Name = name;
        _provinces = provinces;
    }

    [Required]
    public string Name { get; set; }

    private readonly List<Province> _provinces = new List<Province>();
    public IReadOnlyCollection<Province> Provinces => _provinces.AsReadOnly();
}