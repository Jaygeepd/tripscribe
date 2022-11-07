using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.Replication.PgOutput.Messages;

namespace tripscribe.Dal.Models;

[Table("journeys")]
public class Journey
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("title")] public string Title { get; set; }
    
    [Column("description")] public string Description { get; set; }

    [Column("timestamp")] public DateTime Timestamp { get; set; }
}