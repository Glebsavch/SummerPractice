using CyberSportsPortal.Data;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSportsPortal.Core.Services;

public class RatingService(CyberSportsContext context)
{
    private readonly CyberSportsContext _context = context;

    public async Task<List<RatingView>> GetTournamentParticipantsRatingAsync(TournamentFullView tournament)
    {
        var teamIds = tournament.TeamParticipantInfos.Select(x => x.TeamId).ToList();
        var ratings = new List<RatingView>();
        foreach (var id in teamIds.AsParallel())
        {
            var r = await _context.Ratings.FirstAsync(x => x.TeamId == id);
            ratings.Add(new RatingView
            {
                Id = r.Id,
                AtMoment = r.AtMoment,
                Score = r.Score,
                TeamId = r.TeamId,
                TeamName = r.Team.Name,
            });
        }

        return ratings;
    }
}