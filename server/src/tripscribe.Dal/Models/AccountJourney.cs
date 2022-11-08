using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tripscribe.Dal.Models;

[Table("account_journeys")]
public class AccountJourney
{
    [Key] [Column("id")] public int Id { get; set; }
    
    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("journey_id")] public int JourneyId { get; set; }
    
    [ForeignKey(nameof(JourneyId))]  public Journey Journey { get; set; }
}