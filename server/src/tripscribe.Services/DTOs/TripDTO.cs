namespace tripscribe.Services.DTOs;

public class TripDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Timestamp { get; set; }
}