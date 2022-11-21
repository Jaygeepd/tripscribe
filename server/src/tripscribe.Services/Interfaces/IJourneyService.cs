using tripscribe.Services.DTOs;

namespace tripscribe.Services.Services;

public interface IJourneyService
{
    JourneyDTO GetJourney(int id);

    IList<JourneyDTO> GetJourneys(string? title = null, DateTime? startTime = null, DateTime? endTime = null);

    void CreateJourney(JourneyDTO journey);
    
    void UpdateJourney(int id, JourneyDTO journey);

    void DeleteJourney(int id);

    IList<AccountDTO> GetJourneyAccounts(int id);

    IList<ReviewDTO> GetJourneyReviews(int id);
}