using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IJourneyService
{
    IList<JourneyDTO> GetJourney(int id);

    IList<JourneyDTO> GetJourneys(string? Title, DateTime? StartTime, DateTime? EndTime);

    void UpdateJourney(int Id, JourneyDTO journey);
}