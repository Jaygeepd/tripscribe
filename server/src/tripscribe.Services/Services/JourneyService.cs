using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.AccountJourneys;
using tripscribe.Dal.Specifications.Journeys;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class JourneyService : IJourneyService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public JourneyService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public JourneyDTO GetJourney(int id)
    {
        var journeyQuery = _database
            .Get<Journey>()
            .Where(new JourneyByIdSpec(id));

        return _mapper
            .ProjectTo<JourneyDTO>(journeyQuery)
            .SingleOrDefault();
    }
    
    public IList<JourneyDTO> GetJourneys(string? title = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var journeyQuery = _database
            .Get<Journey>()
            .Where(new JourneySearchSpec(title, startDate, endDate));

        return _mapper
            .ProjectTo<JourneyDTO>(journeyQuery)
            .ToList();
        
    }

    public void CreateJourney(JourneyDTO journey)
    {
        var newJourney = new Journey();
        _mapper.Map(journey, newJourney);
        _database.Add(newJourney);
        _database.SaveChanges();
        
        // TODO track user's ID for creation of AccountJourney table entry
    }

    public void UpdateJourney(int id, JourneyDTO journey)
    {

        var currentJourney = _database
            .Get<Journey>()
            .FirstOrDefault(new JourneyByIdSpec(id));

        if (currentJourney == null) throw new NotFoundException("Journey Not Found");

        _mapper.Map(journey, currentJourney);

        _database.SaveChanges();
    }

    public void DeleteJourney(int id)
    {
        var currentJourney = _database
            .Get<Journey>()
            .FirstOrDefault(new JourneyByIdSpec(id));

        if (currentJourney == null) throw new NotFoundException("Journey Not Found");
        
        _database.Delete(currentJourney);
        _database.SaveChanges();
    }

    public IList<AccountDTO> GetJourneyAccounts(int id)
    {
        var accountQuery = _database
            .Get<AccountJourney>()
            .Where(new AccountJourneysByJourneyIdSpec(id))
            .Select(x => x.Account);

        return _mapper
            .ProjectTo<AccountDTO>(accountQuery)
            .ToList();
    }

    public IList<ReviewDTO> GetJourneyReviews(int id)
    {
        var reviewQuery = _database
            .Get<JourneyReview>()
            .Where(new JourneyReviewsByJourneyIdSpec(id))
            .Select(x => x.Review);

        return _mapper
            .ProjectTo<ReviewDTO>(reviewQuery)
            .ToList();
    }

}