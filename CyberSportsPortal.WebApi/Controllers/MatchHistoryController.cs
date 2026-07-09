using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchHistoryController(MatchHistoryService service) : ControllerBase
{
    private readonly MatchHistoryService _service = service;

    [HttpGet("{tournamentId:int}")]
    public async Task<List<MatchHistoryView>> GetAsync([FromRoute] int tournamentId)
    {
        return await _service.GetTournamentMatchHistoryAsync(tournamentId);
    }
}