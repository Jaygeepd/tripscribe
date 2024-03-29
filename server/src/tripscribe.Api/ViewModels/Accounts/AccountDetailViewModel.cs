using tripscribe.Api.ViewModels.Trips;

namespace tripscribe.Api.ViewModels.Accounts;

public class AccountDetailViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IList<TripViewModel>? Trips { get; set; }
}