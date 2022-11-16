namespace tripscribe.Services.DTOs;

public class ReviewDTO
{
    public int Id { get; set; }
    
    public string ReviewText { get; set; }
    
    public int Score { get; set; }
    
    public DateTime Timestamp { get; set; }
    
}