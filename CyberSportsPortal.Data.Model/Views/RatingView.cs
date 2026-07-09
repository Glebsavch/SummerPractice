using System;

namespace CyberSportsPortal.Data.Model.Views;

public class RatingView
{
    public int Id { get; set; }
    public int Score { get; set; }
    public DateTimeOffset AtMoment { get; set; }
    public string TeamName { get; set; }
    public int TeamId { get; set; }
}
