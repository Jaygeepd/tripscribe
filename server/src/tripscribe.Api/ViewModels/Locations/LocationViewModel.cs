using System.Drawing;

namespace tripscribe.Api.ViewModels.Locations;

public class LocationViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateVisited { get; set; }
    public string LocationType { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public int StopId { get; set; }
}