using CyberSportsPortal.Core.Mappers;
using CyberSportsPortal.Data;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSportsPortal.Core.Services;

public class TournamentService(
    CyberSportsContext context,
    TournamentMapper tournamentMapper,
    TournamentTasksService tournamentTasksService)
{
    private readonly CyberSportsContext _context = context;
    private readonly TournamentMapper _tournamentMapper = tournamentMapper;
    private readonly TournamentTasksService _tournamentTasksService = tournamentTasksService;

    public async Task<List<TournamentShortView>> GetTournamentsAsync()
    {
        var tournaments = await _context.Tournaments.ToListAsync();
        return _tournamentMapper.MapList(tournaments);
    }

    public async Task<TournamentFullView> GetLastTournamentAsync()
    {
        return await _context.Tournaments
            .OrderByDescending(x => x.EndDate)
            .Take(1)
            .Select(x => new TournamentFullView
            {
                Id = x.Id,
                EndDate = x.EndDate,
                Name = x.Name,
                Organizer = x.Organizer,
                StartDate = x.StartDate,
                TournamentPrizes = x.TournamentPrizes.Select(p => new TournamentPrizeView
                {
                    Id = p.Id,
                    Place = p.Place,
                    Prize = p.Prize,
                }).ToList(),
                TeamParticipantInfos = x.TeamParticipantInfos.Select(pi => new TournamentParticipantInfoView
                {
                    Id = pi.Id,
                    Place = pi.Place,
                    Standing = pi.Standing,
                    TeamId = pi.TeamId,
                    TeamName = pi.Team.Name
                }).ToList(),
            })
            .FirstAsync();
    }

    public List<TournamentPrize> GetTournamentPrizesAsync(int id)
    {
        return _context.TournamentPrizes.Where(x => x.TournamentId == id).OrderBy(x => x.Place).ToList();
    }

    public async Task<TournamentFullView> GetTournamentByIdAsync(int id)
    {
        return await _context.Tournaments
            .Select(x => new TournamentFullView
            {
                Id = x.Id,
                EndDate = x.EndDate,
                Name = x.Name,
                Organizer = x.Organizer,
                StartDate = x.StartDate,
                TournamentPrizes = x.TournamentPrizes.Select(p => new TournamentPrizeView
                {
                    Id = p.Id,
                    Place = p.Place,
                    Prize = p.Prize,
                }).ToList(),
                TeamParticipantInfos = x.TeamParticipantInfos.Select(pi => new TournamentParticipantInfoView
                {
                    Id = pi.Id,
                    Place = pi.Place,
                    Standing = pi.Standing,
                    TeamId = pi.TeamId,
                    TeamName = pi.Team.Name
                }).ToList(),
            })
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PlayersPerCountry>> GetPlayersPerCountries(int tournamentId)
    {
        var tournament = await _context.Tournaments.FirstOrDefaultAsync(x => x.Id == tournamentId);
        if (tournament == null)
        {
            return null;
        }
        else
        {
            var players = tournament.TeamParticipantInfos.SelectMany(x => x.Team.Players).ToList();
            var countries = players.Select(x => x.Country).Distinct().ToList();

            return countries
                .Select(x =>
                    new PlayersPerCountry
                    {
                        Country = x,
                        PlayersCount = _tournamentTasksService.GetPlayersFromCountryCount(players, x)
                    })
                .OrderByDescending(x => x.PlayersCount)
                .ToList();
        }
            
    }
}