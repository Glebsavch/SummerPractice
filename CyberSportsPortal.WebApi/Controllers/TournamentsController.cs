using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentsController : ControllerBase
{
    private readonly TournamentService _tournamentService;

    public TournamentsController(TournamentService tournamentService)
    {
        _tournamentService = tournamentService;
    }

    [HttpGet("last")]
    public async Task<TournamentFullView> GetLastTournamentAsync()
    {
        return await _tournamentService.GetLastTournamentAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<TournamentFullView> GetTournamentByIdAsync(int id)
    {
        return await _tournamentService.GetTournamentByIdAsync(id);
    }
}