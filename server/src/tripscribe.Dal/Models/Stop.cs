using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("stops")]
public class Stop
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    
    [Column("date_arrived")] public DateTime DateArrived { get; set; }

    [Column("date_departed")] public DateTime DateDeparted { get; set; }
    
    [Column("trip_id")] public int TripId { get; set; }
    
    [ForeignKey(nameof(TripId))]  public Trip Trip { get; set; }

    public ICollection<Location>? StopLocations { get; set; }

    public ICollection<StopReview>? StopReviews { get; set; }
}