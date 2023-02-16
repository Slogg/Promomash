namespace Promomash.Web.Models.Requests;

public class UserRegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAgree { get; set; }
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}

