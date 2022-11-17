using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface ILocationService
{
    LocationDTO GetLocation(int id);
    
    IList<LocationDTO> GetLocations(string? name, string? locationType, DateTime? startDate, DateTime? endDate,
        int? stopId);

    void CreateLocation(LocationDTO location);
    
    void UpdateLocation(int Id, LocationDTO location);

    void DeleteLocation(int id);

    IList<ReviewDTO> GetLocationReviews(int id);
}