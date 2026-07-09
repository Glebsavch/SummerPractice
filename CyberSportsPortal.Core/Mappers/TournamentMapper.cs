using CyberSportsPortal.Core.Services;
using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Views;
using System.Collections.Generic;
using System.Linq;

namespace CyberSportsPortal.Core.Mappers;

public class TournamentMapper
{
    private readonly TournamentTasksService _tournamentTasksService;

    public TournamentMapper(TournamentTasksService tournamentTasksService)
    {
        _tournamentTasksService = tournamentTasksService;
    }

    public TournamentShortView Map(Tournament tournament)
    {
        return new TournamentShortView
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Organizer = tournament.Organizer,
            Status = _tournamentTasksService.GetTournamentStatus(tournament)
        };
    }

    public List<TournamentShortView> MapList(List<Tournament> tournaments)
    {
        return tournaments.Select(x => Map(x)).ToList();
    }
}