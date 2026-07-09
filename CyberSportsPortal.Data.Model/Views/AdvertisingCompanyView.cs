using System.Collections.Generic;

namespace CyberSportsPortal.Data.Model.Views;

public class AdvertisingCompanyView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AdvertisementLink { get; set; }
    public List<AdvertisementPaymentInfoView> PaymentInfo { get; set; }
}
