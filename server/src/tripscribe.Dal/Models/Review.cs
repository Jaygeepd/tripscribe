using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("reviews")]
public class Review
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("review_text")] public string ReviewText { get; set; }
    
    [Column("score")] public int Score { get; set; }

    [Column("timestamp")] public DateTime Timestamp { get; set; }

    public ICollection<TripReview> TripReviews { get; set; }
    public ICollection<StopReview> StopReviews { get; set; }
    public ICollection<LocationReview> LocationReviews { get; set; }
}