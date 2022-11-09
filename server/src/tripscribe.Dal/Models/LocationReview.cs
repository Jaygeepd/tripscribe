using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("location_reviews")]
public class LocationReview
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("location_id")] public int LocationId { get; set; }

    [ForeignKey(nameof(LocationId))] public Location Location { get; set; }

    [Column("review_id")] public int ReviewId { get; set; }
    
    [ForeignKey(nameof(ReviewId))] public Review Review { get; set; }
    
}