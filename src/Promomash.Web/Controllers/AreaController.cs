using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Promomash.Core.Interfaces;
using Promomash.Web.Models.Responses;

namespace Promomash.Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AreaController : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly IProvinceService _provinceService;
    private readonly IMapper _mapper;

    public AreaController(ICountryService countryService, IProvinceService provinceService, IMapper mapper)
    {
        _countryService = countryService;
        _provinceService = provinceService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
    {
        var countries = await _countryService.GetCountriesAsync(cancellationToken);
        var countryResponse = _mapper.Map<List<CountryResponse>>(countries);

        return Ok(countryResponse);
    }

    [HttpGet("{countryId:int}")]
    public async Task<IActionResult> GetProvinces(int countryId, CancellationToken cancellationToken)
    {
        var provinces = await _provinceService.GetProvincesByCountryIdAsync(countryId, cancellationToken);
        var provinceResponse = _mapper.Map<List<ProvinceResponse>>(provinces);

        return Ok(provinceResponse);
    }
}
