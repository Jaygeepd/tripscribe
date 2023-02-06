namespace tripscribe.Services.DTOs;

public class TripDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Timestamp { get; set; }
    
    public Boolean PublicView { get; set; }
    
    public IList<AccountDTO>? Accounts { get; set; }
    
    public IList<StopDTO>? Stops { get; set; }
}