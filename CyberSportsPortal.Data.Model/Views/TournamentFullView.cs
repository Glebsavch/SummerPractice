using System.Collections.Generic;
using System;

namespace CyberSportsPortal.Data.Model.Views;

public class TournamentFullView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Organizer { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public List<TournamentParticipantInfoView> TeamParticipantInfos { get; set; }
    public List<TournamentPrizeView> TournamentPrizes { get; set; }
}
