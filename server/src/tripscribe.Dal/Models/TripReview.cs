using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("trip_reviews")]
public class TripReview
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("trip_id")] public int TripId { get; set; }

    [ForeignKey(nameof(TripId))] public Trip Trip { get; set; }

    [Column("review_id")] public int ReviewId { get; set; }
    
    [ForeignKey(nameof(ReviewId))] public Review Review { get; set; }
    
}