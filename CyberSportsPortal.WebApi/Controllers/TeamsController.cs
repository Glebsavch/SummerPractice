using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly TeamService _teamsService;
    private readonly TeamTasksService _teamTasksService;

    public TeamsController(TeamService teamsService, TeamTasksService teamTasksService)
    {
        _teamsService = teamsService;
        _teamTasksService = teamTasksService;
    }

    [HttpGet]
    public async Task<List<TeamView>> Get()
    {
        return await _teamsService.GetTeamsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<TeamView> Get([FromRoute] int id)
    {
        return await _teamsService.GetTeamByIdAsync(id);
    }

    [HttpGet("{teamId:int}/{year:int}/income")]
    public async Task<int> GetTeamIncomeForYear(int teamId, int year)
    {
        var team = await _teamsService.GetTeamByIdAsync(teamId);
        return _teamTasksService.GetTeamIncomeForYear(team, year);
    }
}