using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("trips")]
public class Trip
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("title")] public string Title { get; set; }
    
    [Column("description")] public string Description { get; set; }

    [Column("timestamp")] public DateTime Timestamp { get; set; }
    
    [Column("public_view")] public Boolean PublicView { get; set; }
    
    public ICollection<Stop> TripStops { get; set; }

    public ICollection<AccountTrip> AccountTrips { get; set; }
    
    public ICollection<TripReview> TripReviews { get; set; }
}