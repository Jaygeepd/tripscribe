using System.Drawing;
using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface ILocationService
{
    LocationDTO GetLocation(int id);
    
    IList<LocationDTO> GetLocations(string? name = null, string? locationType = null, DateTime? startDate = null, DateTime? endDate = null,
        Point? GeoLocation = null, int? stopId = null);

    void CreateLocation(LocationDTO location);
    
    void UpdateLocation(int Id, LocationDTO location);

    void DeleteLocation(int id);

    IList<ReviewDTO> GetLocationReviews(int id);
}