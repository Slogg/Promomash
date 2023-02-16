using Promomash.Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Promomash.Core.Entities;

[Table("Users")]
public class User : BaseEntity<Guid>, IAggregateRoot
{
    public User() { }

    public User(Guid id, string email, string password, int provinceId)
    {
        Id = id;
        Email = email;
        Password = password;
        ProvinceId = provinceId;
    }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int ProvinceId { get; set; }

    public virtual Province Province { get; set; }
}

