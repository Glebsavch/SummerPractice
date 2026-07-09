using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly PlayerService _playerService;
    private readonly PlayerTasksService _playerTasksService;

    public PlayersController(PlayerService service, PlayerTasksService playerTasksService)
    {
        _playerService = service;
        _playerTasksService = playerTasksService;
    }

    [HttpGet]
    public async Task<List<PlayerView>> GetAsync()
    {
        return await _playerService.GetPlayersAsync();
    }

    [HttpGet("filter")]
    public async Task<List<PlayerView>> GetAsync([FromQuery] string filter)
    {
        var players = await _playerService.GetPlayersAsync();
        return _playerTasksService.FilterPlayers(players, filter);
    }
}