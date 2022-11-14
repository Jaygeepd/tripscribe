using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IJourneyService
{
    IList<JourneyDTO> GetJourneys(int? Id, string? Title, DateTime? StartTime, DateTime? EndTime);

    void UpdateJourney(int Id, JourneyDTO journey);
}