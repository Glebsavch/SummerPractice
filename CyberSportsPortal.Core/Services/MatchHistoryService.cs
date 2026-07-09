using CyberSportsPortal.Data;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSportsPortal.Core.Services;

public class MatchHistoryService(CyberSportsContext context)
{
    private readonly CyberSportsContext _context = context;

    public async Task<List<MatchHistoryView>> GetTournamentMatchHistoryAsync(int tournamentId)
    {
        return await _context.MatchHistories
            .Where(x => x.TournamentId == tournamentId)
            .Select(x => new MatchHistoryView
            {
                Id = x.Id,
                Date = x.Date,
                LoserId = x.LoserId,
                WinnerId = x.WinnerId,
                TournamentId = x.TournamentId,
                Loser = x.Loser.Name,
                Winner = x.Winner.Name,
            })
            .ToListAsync();
    }
}