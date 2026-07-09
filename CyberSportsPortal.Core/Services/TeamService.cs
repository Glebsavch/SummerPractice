using CyberSportsPortal.Core.Mappers;
using CyberSportsPortal.Data;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace CyberSportsPortal.Core.Services;

public class TeamService
{
    private readonly CyberSportsContext _context;
    private readonly TeamMapper _mapper;

    public TeamService(CyberSportsContext context, TeamMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TeamView>> GetTeamsAsync()
    {
        return await _context.Teams
            .Select(x => new TeamView
            {
                Id = x.Id,
                MatchesLost = x.MatchesLost.Select(ml => new MatchHistoryView
                {
                    Date = ml.Date,
                    Id = ml.Id,
                    LoserId = ml.LoserId,
                    TournamentId = ml.TournamentId,
                    WinnerId = ml.WinnerId,
                    Loser = ml.Loser.Name,
                    Winner = ml.Winner.Name
                }).ToList(),
                MatchesWon = x.MatchesWon.Select(mw => new MatchHistoryView
                {
                    Date = mw.Date,
                    Id = mw.Id,
                    LoserId = mw.LoserId,
                    TournamentId = mw.TournamentId,
                    WinnerId = mw.WinnerId,
                    Loser = mw.Loser.Name,
                    Winner = mw.Winner.Name
                }).ToList(),
                Name = x.Name,
                Players = x.Players.Select(pl => new PlayerView
                {
                    Id = pl.Id,
                    Country = pl.Country,
                    NickName = pl.Nickname,
                    CombinedName = $"{pl.Name} {pl.Surname}",
                    TeamName = pl.Team.Name
                }).ToList(),
                TeamRatings = x.TeamRatings.Select(tr => new RatingView
                {
                    Id = tr.Id,
                    AtMoment = tr.AtMoment,
                    Score = tr.Score,
                    TeamId = tr.TeamId,
                    TeamName = tr.Team.Name,
                }).ToList(),
                TeamTournamentResults = x.TeamTournamentResults.Select(ttr => new TournamentParticipantInfoView
                {
                    Id = ttr.Id,
                    Place = ttr.Place,
                    Standing = ttr.Standing,
                    TeamId = ttr.TeamId,
                    TeamName = ttr.Team.Name,
                    // Новые поля
                    TournamnetId = ttr.TournamentId,
                    TournamentStartDate = ttr.Tournament.StartDate,
                    TournamentEndDate = ttr.Tournament.EndDate,
                    Prize = ttr.Tournament.TournamentPrizes
                        .Where(p => p.Place == ttr.Place)
                        .Select(p => p.Prize)
                        .FirstOrDefault()
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<TeamView> GetTeamByIdAsync(int id)
    {
        return await _context.Teams
            .Select(x => new TeamView
            {
                Id = x.Id,
                MatchesLost = x.MatchesLost.Select(ml => new MatchHistoryView
                {
                    Date = ml.Date,
                    Id = ml.Id,
                    LoserId = ml.LoserId,
                    TournamentId = ml.TournamentId,
                    WinnerId = ml.WinnerId,
                    Loser = ml.Loser.Name,
                    Winner = ml.Winner.Name
                }).ToList(),
                MatchesWon = x.MatchesWon.Select(mw => new MatchHistoryView
                {
                    Date = mw.Date,
                    Id = mw.Id,
                    LoserId = mw.LoserId,
                    TournamentId = mw.TournamentId,
                    WinnerId = mw.WinnerId,
                    Loser = mw.Loser.Name,
                    Winner = mw.Winner.Name
                }).ToList(),
                Name = x.Name,
                Players = x.Players.Select(pl => new PlayerView
                {
                    Id = pl.Id,
                    Country = pl.Country,
                    NickName = pl.Nickname,
                    CombinedName = $"{pl.Name} {pl.Surname}",
                    TeamName = pl.Team.Name
                }).ToList(),
                TeamRatings = x.TeamRatings.Select(tr => new RatingView
                {
                    Id = tr.Id,
                    AtMoment = tr.AtMoment,
                    Score = tr.Score,
                    TeamId = tr.TeamId,
                    TeamName = tr.Team.Name,
                }).ToList(),
                TeamTournamentResults = x.TeamTournamentResults.Select(ttr => new TournamentParticipantInfoView
                {
                    Id = ttr.Id,
                    Place = ttr.Place,
                    Standing = ttr.Standing,
                    TeamId = ttr.TeamId,
                    TeamName = ttr.Team.Name,
                    // Новые поля
                    TournamnetId = ttr.TournamentId,
                    TournamentStartDate = ttr.Tournament.StartDate,
                    TournamentEndDate = ttr.Tournament.EndDate,
                    Prize = ttr.Tournament.TournamentPrizes
                        .Where(p => p.Place == ttr.Place)
                        .Select(p => p.Prize)
                        .FirstOrDefault()
                }).ToList()
            })
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TeamWithVictoryProbabilities>> GetTeamsWithVictoryProbabilitiesForTournament(List<int> teamIds)
    {
        var result = new List<TeamWithVictoryProbabilities>();
        var teams = await _context.Teams.Where(x => teamIds.Contains(x.Id)).ToListAsync();
        foreach (var team in teams)
        {
            var probabilities = await _context.ProbabilityOfVictories
                .Where(x => x.LoserId == team.Id && teamIds.Contains(x.WinnerId) || x.WinnerId == team.Id && teamIds.Contains(x.LoserId)).ToListAsync();
            result.Add(_mapper.Map(team, probabilities));
        }

        return result;
    }
}