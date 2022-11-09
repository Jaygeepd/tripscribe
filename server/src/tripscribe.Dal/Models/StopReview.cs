using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("stop_reviews")]
public class StopReview
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("stop_id")] public int StopId { get; set; }

    [ForeignKey(nameof(StopId))] public Stop Stop { get; set; }

    [Column("review_id")] public int ReviewId { get; set; }
    
    [ForeignKey(nameof(ReviewId))] public Review Review { get; set; }
    
}