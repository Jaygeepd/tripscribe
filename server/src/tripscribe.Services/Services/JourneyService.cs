using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using tripscribe.Dal.Specifications.Journeys;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class JourneyService : IJourneyService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public JourneyService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public IList<JourneyDTO> GetJourneys(int? id = null, string? title = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var journeyQuery = _database
            .Get<Journey>()
            .Where(new JourneySearchSpec(id, title, startDate, endDate));

        return _mapper
            .ProjectTo<JourneyDTO>(journeyQuery)
            .ToList();
        
    }

    public void UpdateJourney(int id, JourneyDTO journey)
    {

        var currentJourney = _database
            .Get<Journey>()
            .FirstOrDefault(new JourneyByIdSpec(id));

        if (currentJourney == null) throw new Exception("Not Found");

        _mapper.Map(journey, currentJourney);

        _database.SaveChanges();
    }

}