using System.Collections.Generic;

namespace CyberSportsPortal.Data.Model.Views;

public class TeamView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlayerView> Players { get; set; }
    public List<TournamentParticipantInfoView> TeamTournamentResults { get; set; }
    public List<RatingView> TeamRatings { get; set; }
    public List<MatchHistoryView> MatchesWon { get; set; }
    public List<MatchHistoryView> MatchesLost { get; set; }
}
