using System.Drawing;

namespace tripscribe.Services.DTOs;

public class LocationDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string LocationType { get; set; }
    
    public DateTime DateVisited { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public int StopId { get; set; }
}
