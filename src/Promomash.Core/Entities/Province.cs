using Promomash.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promomash.Core.Entities;

[Table("Provinces")]
public class Province : BaseEntity<int>, IAggregateRoot
{
    public Province() { }

    public Province(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Required]
    public string Name { get; set; }

    public int CountryId { get; set; }
}