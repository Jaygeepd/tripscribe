using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface ITripService
{
    TripDTO? GetTrip(int id);

    IList<TripDTO> GetTrips(string? title = null, DateTime? startTime = null, DateTime? endTime = null);

    void CreateTrip(TripDTO journey);
    
    void UpdateTrip(int id, TripDTO journey);

    void DeleteTrip(int id);

    IList<AccountDTO> GetTripAccounts(int id);

    IList<ReviewDTO> GetTripReviews(int id);

    IList<StopDTO> GetTripStops(int id);
}