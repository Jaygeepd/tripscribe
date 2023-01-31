namespace tripscribe.Services.DTOs;

public class StopDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTime DateArrived { get; set; }
    
    public DateTime DateDeparted { get; set; }
    
    public IList<LocationDTO>? Locations { get; set; }

    public int TripId { get; set; }
}