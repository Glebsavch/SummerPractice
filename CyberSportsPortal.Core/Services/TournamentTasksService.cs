using CyberSportsPortal.Data.Entities;
using CyberSportsPortal.Data.Model.Enums;
using CyberSportsPortal.Data.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace CyberSportsPortal.Core.Services;

public class TournamentTasksService
{
    public string GetTournamentStatus(Tournament tournament)
    {
        // добавил DateTime, чтобы узнать время сейчас и сравнения с StartDate и EndDate по условию задания 1
        DateTime today = DateTime.UtcNow.Date;      

        if (today < tournament.StartDate)
        {
            return "Не начался";
        }
        else if (today > tournament.EndDate)
        {
            return "Окончился";
        }
        else
        {
            return "В процессе";
        }
    }

    public int GetPlayersFromCountryCount(List<Player> players, Country country)
    {
        // LINQ Count с предикатом для подсчёта игроков из страны counrty
        return players.Count(p => p.Country == country);
    }

    public string GetTeamParticipantsNameString(List<string> teamNames)
    {
        // с помощью Join расставим разделители между элементами, а не после каждого
        return string.Join(", ", teamNames);
    }

    public int ComparePrizes(string prizeA, string prizeB)
    {
        // Т.к. призы хранятся как строка, то нужно распарсить в decimal (decimal для точности)
        var valueA = ParsePrize(prizeA);
        var valueB = ParsePrize(prizeB);

        return Math.Sign(valueA.CompareTo(valueB)); // Math.Sign(int) гарантированно возвратит -1, 0 или 1
    }

    // Оставляем только цифры, точку (минус не учитываем, т.к. предполагаем, что призовые не могут быть отрицательными)
    private static decimal ParsePrize(string prize)
    {
        if (string.IsNullOrWhiteSpace(prize))
        {
            return 0m;
        }

        var cleaned = new string(prize.Where(c => char.IsDigit(c) || c == '.').ToArray());

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : 0m; // нуль, если не удаётся распарсить
    }

    public Dictionary<int, decimal> GetTournamentVictoryProbabilities(List<TeamWithVictoryProbabilities> teams, Dictionary<int, int> standings)
    {
        return new Dictionary<int, decimal>();
    }
}