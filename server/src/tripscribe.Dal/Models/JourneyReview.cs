using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("journey_reviews")]
public class JourneyReview
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("journey_id")] public int JourneyId { get; set; }

    [ForeignKey(nameof(JourneyId))] public Journey Journey { get; set; }

    [Column("review_id")] public int ReviewId { get; set; }
    
    [ForeignKey(nameof(ReviewId))] public Review Review { get; set; }
    
}