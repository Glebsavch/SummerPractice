using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementsController : ControllerBase
{
    private readonly AdvertisementService _advertisementService;
    private readonly AdvertisementTasksService _advertisementTasksService;

    public AdvertisementsController(AdvertisementService advertisementService, AdvertisementTasksService advertisementTasksService)
    {
        _advertisementService = advertisementService;
        _advertisementTasksService = advertisementTasksService;
    }

    [HttpGet("companies")]
    public async Task<List<AdvertisingCompanyView>> GetCompaniesAsync()
    {
        return await _advertisementService.GetAdvertisingCompaniesAsync();
    }

    [HttpGet("probabilities")]
    public async Task<List<KeyValuePair<int, int>>> GetProbabilities()
    {
        var advertisingCompanies = await _advertisementService.GetAdvertisingCompaniesAsync();
        return _advertisementTasksService.GetProbabilities(advertisingCompanies);
    }
}