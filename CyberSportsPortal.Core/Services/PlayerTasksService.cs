using CyberSportsPortal.Data.Model.Views;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CyberSportsPortal.Core.Services;

public class PlayerTasksService
{
    public List<PlayerView> FilterPlayers(List<PlayerView> players, string filter)
    {
        if (players == null)
        {
            return new List<PlayerView>();
        }

        if (string.IsNullOrEmpty(filter))
        {
            return players;
        }

        return players
            .Where(p => Contains(p.NickName, filter) || Contains(p.CombinedName, filter))
            .ToList();
    }

    // NickName и CombinedName могут быть null, а также filter должен быть не регистрозависимым (т.е. чтобы "иван" находил "Иван")
    private static bool Contains(string source, string filter)
    {
        return !string.IsNullOrEmpty(source) && source.Contains(filter, StringComparison.OrdinalIgnoreCase);
    }
}

