using System.Drawing;
using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Locations;
using tripscribe.Dal.Specifications.Reviews;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class LocationService : ILocationService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public LocationService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public LocationDTO GetLocation(int id)
    {
        var locationQuery = _database
            .Get<Location>()
            .Where(new LocationByIdSpec(id));

        return _mapper
            .ProjectTo<LocationDTO>(locationQuery)
            .SingleOrDefault();
    }

    public IList<LocationDTO> GetLocations(string? name = null, string? locationType = null, DateTime? startDate = null, DateTime? endDate = null, double? latitude = null, double? longitude = null, int? stopId = null)
    {
        var locationQuery = _database
            .Get<Location>()
            .Where(new LocationSearchSpec(name, locationType, startDate, endDate, latitude, longitude, stopId));

        return _mapper
            .ProjectTo<LocationDTO>(locationQuery)
            .ToList();
        
    }

    public void CreateLocation(LocationDTO location)
    {
        
        var newLocation = _mapper.Map<Location>(location);
        _database.Add(newLocation);
        _database.SaveChanges();
    }

    public void UpdateLocation(int id, LocationDTO location)
    {

        var currentLocation = _database
            .Get<Location>()
            .FirstOrDefault(new LocationByIdSpec(id));

        if (currentLocation == null) throw new NotFoundException("Location Not Found");

        _mapper.Map(location, currentLocation);

        _database.SaveChanges();
    }

    public void DeleteLocation(int id)
    {
        var currentLoc = _database
            .Get<Location>()
            .FirstOrDefault(new LocationByIdSpec(id));

        if (currentLoc == null) throw new NotFoundException("Location Not Found");
        
        _database.Delete(currentLoc);
        _database.SaveChanges();
    }

    public IList<ReviewDTO> GetLocationReviews(int id)
    {
        var locationQuery = _database
            .Get<LocationReview>()
            .Where(new LocationReviewsByLocationIdSpec(id))
            .Select(x => x.Review);

        return _mapper
            .ProjectTo<ReviewDTO>(locationQuery)
            .ToList();
    }
}