using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("locations")]
public class Locaion
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    
    [Column("date_arrived")] public DateTime DateArrived { get; set; }

    [Column("location_type")] public string LocationType { get; set; }
    
    [Column("stop_id")] public int StopId { get; set; }
}