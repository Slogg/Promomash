using AutoMapper;
using Promomash.Core.Entities;
using Promomash.Web.Models.Responses;

namespace Promomash.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Country, CountryResponse>();
        CreateMap<Province, ProvinceResponse>();
    }
}