using CyberSportsPortal.Data.Model.Views;
using System.Collections.Generic;
using System.Linq; // чтобы методы расширения работали

namespace CyberSportsPortal.Core.Services;

public class TeamTasksService
{
    public int GetTeamIncomeForYear(TeamView team, int year)
    {
        // добавлены также новые поля даты турнира и сумма призовых в TournamentParticipantInfoView.cs

        if (team?.TeamTournamentResults == null)
        {
            return 0;
        }

        // турнир завершён и приз выплачен в том календарном году, в котором прошла его дата окончания
        return team.TeamTournamentResults
            .Where(result => result.TournamentEndDate.Year == year)
            .Sum(result => result.Prize);
    }

    public List<RatingView> GetNewRatings(List<MatchHistoryView> matches, List<RatingView> oldRatings)
    {
        return oldRatings;
    }
}