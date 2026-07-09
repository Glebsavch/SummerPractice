using CyberSportsPortal.Data.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSportsPortal.Core.Services;

public class AdvertisementTasksService
{
    public List<KeyValuePair<int, int>> GetProbabilities(List<AdvertisingCompanyView> companies)
    {
        var result = new List<KeyValuePair<int, int>>();

        if (companies == null || companies.Count == 0)
        {
            return result;
        }

        // записываем границы года
        var today = DateTime.Now;
        var yearStart = new DateTime(today.Year, 1, 1);
        var yearEnd = new DateTime(today.Year, 12, 31, 23, 59, 59, 999);

        // словарь: id компании, сумма её платежей за год
        var yearlySumByCompany = companies.ToDictionary(
            c => c.Id,
            c => c.PaymentInfo?
                .Where(p => p.PaymentDate >= yearStart && p.PaymentDate <= yearEnd)
                .Sum(p => p.PaymentSum) ?? 0m); // на случай, если PaymentInfo == null

        var total = yearlySumByCompany.Values.Sum(); // все платежи всех компаний

        foreach (var company in companies)
        {
            int probability;

            if (total <= 0) // на случай, если ни одна компания не платила
            {
                probability = 1;
            }
            else
            {
                var percent = (int)Math.Floor(yearlySumByCompany[company.Id] / total * 100m);
                probability = Math.Max(percent, 1);
            }

            result.Add(new KeyValuePair<int, int>(company.Id, probability));
        }

        return result;
    }
}