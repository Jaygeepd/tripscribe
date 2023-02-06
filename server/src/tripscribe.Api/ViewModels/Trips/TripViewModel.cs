using tripscribe.Api.ViewModels.Accounts;
using tripscribe.Api.ViewModels.Stop;

namespace tripscribe.Api.ViewModels.Trips;

public class TripViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public Boolean PublicView { get; set; }
}