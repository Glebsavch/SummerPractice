using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.AspNetCore.Mvc;

namespace CyberSportsPortal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly RatingService _ratingService;
    private readonly TournamentService _tournamentService;

    public RatingsController(TournamentService tournamentServiceservice, RatingService ratingService)
    {
        _tournamentService = tournamentServiceservice;
        _ratingService = ratingService;
    }

    [HttpGet("{id:int}")]
    public async Task<List<RatingView>> GetTournamentParticipantsRatingAsync(int id)
    {
        var tournamentView = await _tournamentService.GetTournamentByIdAsync(id);
        return await _ratingService.GetTournamentParticipantsRatingAsync(tournamentView);
    }
}