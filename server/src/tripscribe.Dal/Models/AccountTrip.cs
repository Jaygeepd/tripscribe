using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tripscribe.Dal.Models;

[Table("account_trip")]
public class AccountTrip
{
    [Key] [Column("id")] public int Id { get; set; }
    
    [Column("account_id")] public int AccountId { get; set; }
    
    [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    
    [Column("trip_id")] public int TripId { get; set; }
    
    [ForeignKey(nameof(TripId))]  public Trip Trip { get; set; }
}