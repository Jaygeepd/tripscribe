namespace tripscribe.Api.ViewModels.Reviews;

public class ReviewViewModel
{
    public int Id { get; set; }
    public string ReviewText { get; set; }
    public int Score { get; set; }
    public DateTime Timestamp { get; set; }
}