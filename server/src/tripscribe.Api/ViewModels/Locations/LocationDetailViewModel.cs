namespace tripscribe.Api.ViewModels.Locations;

public class LocationDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    public string LocationType { get; set; }
    public int StopId { get; set; }
}