namespace tripscribe.Api.ViewModels.Stop;

public class StopDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateArrived { get; set; }
    public DateTime DateDeparted { get; set; }
    public int TripId { get; set; }
}