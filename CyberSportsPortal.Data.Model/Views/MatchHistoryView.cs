using System;

namespace CyberSportsPortal.Data.Model.Views;

public class MatchHistoryView
{
    public int Id { get; set; }
    public int WinnerId { get; set; }
    public int LoserId { get; set; }
    public int TournamentId { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Winner { get; set; }
    public string Loser { get; set; }
}
