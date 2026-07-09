using CyberSportsPortal.Data;
using CyberSportsPortal.Data.Model.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSportsPortal.Core.Services;

public class AdvertisementService(CyberSportsContext context)
{
    private readonly CyberSportsContext _context = context;

    public async Task<List<AdvertisingCompanyView>> GetAdvertisingCompaniesAsync()
    {
        return await _context.AdvertisingCompanies
            .Select(x => new AdvertisingCompanyView
            {
                Id = x.Id,
                AdvertisementLink = x.AdvertisementLink,
                Name = x.Name,
                PaymentInfo = x.AdvertisementPaymentInfos.Select(x => new AdvertisementPaymentInfoView
                {
                    Id = x.Id,
                    PaymentDate = x.PaymentDate,
                    PaymentSum = x.PaymentSum,
                }).ToList(),
            })
            .ToListAsync();
    }
}