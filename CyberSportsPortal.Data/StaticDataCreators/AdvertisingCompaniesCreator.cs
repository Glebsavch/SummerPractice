using CyberSportsPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CyberSportsPortal.Data.StaticDataCreators
{
    public class AdvertisingCompaniesCreator
    {
        public static void CreateCompanies(ModelBuilder builder)
        {
            builder.Entity<AdvertisingCompany>().HasData(
                new AdvertisingCompany()
                {
                    Id = 1,
                    Name = "VeryBigCompany",
                    AdvertisementLink = "videos/Cat.mp4"
                },
                new AdvertisingCompany()
                {
                    Id = 2,
                    Name = "SmallCompany",
                    AdvertisementLink = "videos/Cat2.mp4"
                }
             );

            builder.Entity<AdvertisementPaymentInfo>().HasData(
                new AdvertisementPaymentInfo()
                {
                    Id = 1,
                    AdvertisingCompanyId = 1,
                    PaymentDate = new DateTime(new DateOnly(2026, 7, 1), new TimeOnly(), DateTimeKind.Utc),
                    PaymentSum = 10000
                },
                new AdvertisementPaymentInfo()
                {
                    Id = 2,
                    AdvertisingCompanyId = 2,
                    PaymentDate = new DateTime(new DateOnly(2026, 7, 1), new TimeOnly(), DateTimeKind.Utc),
                    PaymentSum = 1000
                });
        }
    }
}
