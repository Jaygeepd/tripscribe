using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("accounts")]
public class Account
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("first_name")] public string FirstName { get; set; }
    
    [Column("last_name")] public string LastName { get; set; }

    [Column("email")] public string Email { get; set; }
    
    [Column("password")] public string Password { get; set; }
    
    public ICollection<AccountJourney> AccountJourneys { get; set; }
    
    public ICollection<JourneyReview> JourneyReviews { get; set; }
    
    public ICollection<StopReview> StopReviews { get; set; }
    
    public ICollection<LocationReview> LocationReviews { get; set; }
}