using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Dal.Specifications.Stops;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class StopService : IStopService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public StopService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public StopDTO GetStop(int id)
    {
        var stopQuery = _database
            .Get<Stop>()
            .Where(new StopByIdSpec(id));

        return _mapper
            .ProjectTo<StopDTO>(stopQuery)
            .SingleOrDefault();
    }

    public IList<StopDTO> GetStops(string? name = null, DateTime? startArrivedTime = null, 
        DateTime? endArrivedTime = null, DateTime? startDepartedTime = null, DateTime? endDepartedTime = null, int? journeyId = null)
    {
        var stopQuery = _database
            .Get<Stop>()
            .Where(new StopSearchSpec(name, startArrivedTime, endArrivedTime, startDepartedTime, endDepartedTime, journeyId));

        return _mapper
            .ProjectTo<StopDTO>(stopQuery)
            .ToList();
        
    }

    public void CreateStop(StopDTO stopDetails)
    {
        var stop = new Stop();
        _mapper.Map(stopDetails, stop);
        _database.Add(stop);
        _database.SaveChanges();
    }

    public void UpdateStop(int id, StopDTO stop)
    {

        var currentStop = _database
            .Get<Stop>()
            .FirstOrDefault(new StopByIdSpec(id));

        if (currentStop == null) throw new NotFoundException("Stop Not Found");

        _mapper.Map(stop, currentStop);

        _database.SaveChanges();
    }

    public void DeleteStop(int id)
    {
        var currentStop = _database
            .Get<Stop>()
            .FirstOrDefault(new StopByIdSpec(id));

        if (currentStop == null) throw new NotFoundException("Stop Not Found");
        
        _database.Delete(currentStop);
        _database.SaveChanges();
    }

    public IList<ReviewDTO> GetStopReviews(int id)
    {
        var reviewQuery = _database
            .Get<StopReview>()
            .Where(new StopReviewsByStopIdSpec(id))
            .Select(x => x.Review);

        return _mapper
            .ProjectTo<ReviewDTO>(reviewQuery)
            .ToList();
    }

}