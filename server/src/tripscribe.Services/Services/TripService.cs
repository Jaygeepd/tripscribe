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

public class TripService : ITripService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public TripService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public TripDTO GetTrip(int id)
    {
        var tripQuery = _database
            .Get<Trip>()
            .Where(new TripByIdSpec(id));

        return _mapper
            .ProjectTo<TripDTO>(tripQuery)
            .SingleOrDefault();
    }
    
    public IList<TripDTO> GetTrips(string? title = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var tripQuery = _database
            .Get<Trip>()
            .Where(new TripSearchSpec(title, startDate, endDate));

        return _mapper
            .ProjectTo<TripDTO>(tripQuery)
            .ToList();
        
    }

    public void CreateTrip(TripDTO trip)
    {
        var newTrip = _mapper.Map<Trip>(trip);
        _database.Add(newTrip);
        _database.SaveChanges();
        
        // TODO track user's ID for creation of AccountTrip table entry
    }

    public void UpdateTrip(int id, TripDTO trip)
    {

        var currentTrip = _database
            .Get<Trip>()
            .FirstOrDefault(new TripByIdSpec(id));

        if (currentTrip == null) throw new NotFoundException("Trip Not Found");

        _mapper.Map(trip, currentTrip);

        _database.SaveChanges();
    }

    public void DeleteTrip(int id)
    {
        var currentTrip = _database
            .Get<Trip>()
            .FirstOrDefault(new TripByIdSpec(id));

        if (currentTrip == null) throw new NotFoundException("Trip Not Found");
        
        _database.Delete(currentTrip);
        _database.SaveChanges();
    }

    public IList<AccountDTO> GetTripAccounts(int id)
    {
        var accountQuery = _database
            .Get<AccountTrip>()
            .Where(new AccountTripsByTripIdSpec(id))
            .Select(x => x.Account);

        return _mapper
            .ProjectTo<AccountDTO>(accountQuery)
            .ToList();
    }

    public IList<ReviewDTO> GetTripReviews(int id)
    {
        var reviewQuery = _database
            .Get<TripReview>()
            .Where(new TripReviewsByTripIdSpec(id))
            .Select(x => x.Review);

        return _mapper
            .ProjectTo<ReviewDTO>(reviewQuery)
            .ToList();
    }

}