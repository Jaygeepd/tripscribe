using System.ComponentModel.DataAnnotations.Schema;

namespace tripscribe.Dal.Models;

public class AccountJourney
{
    public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    public int JourneyId { get; set; }
    
    [ForeignKey(nameof(JourneyId))] public Journey Journey { get; set; }
}