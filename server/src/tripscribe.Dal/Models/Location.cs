using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Npgsql.Replication.PgOutput.Messages;
using NpgsqlTypes;

namespace tripscribe.Dal.Models;

[Table("locations")]
public class Location
{
    [Key] [Column("id")] public int Id { get; set; }

    [Column("name")] public string Name { get; set; }
    
    [Column("date_visited")] public DateTime DateVisited { get; set; }

    [Column("location_type")] public string LocationType { get; set; }
    
    [Column("geo_location")] public NpgsqlPoint GeoLocation { get; set; }
    
    [Column("stop_id")] public int StopId { get; set; }
    [ForeignKey(nameof(StopId))] public Stop Stop { get; set; }
    
    public ICollection<LocationReview> LocationReviews { get; set; }
}