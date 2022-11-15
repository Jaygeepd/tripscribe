using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Locations;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class LocationService : ILocationService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public LocationService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public IList<LocationDTO> GetLocation(int id)
    {
        var locationQuery = _database
            .Get<Location>()
            .Where(new LocationByIdSpec(id));

        return _mapper
            .ProjectTo<LocationDTO>(locationQuery)
            .ToList();
    }

    public IList<LocationDTO> GetLocations(string? name = null, string? locationType = null, DateTime? startDate = null, DateTime? endDate = null, int? stopId = null)
    {
        var locationQuery = _database
            .Get<Location>()
            .Where(new LocationSearchSpec(name, locationType, startDate, endDate, stopId));

        return _mapper
            .ProjectTo<LocationDTO>(locationQuery)
            .ToList();
        
    }

    public void UpdateLocation(int id, LocationDTO location)
    {

        var currentLocation = _database
            .Get<Location>()
            .FirstOrDefault(new LocationByIdSpec(id));

        if (currentLocation == null) throw new Exception("Not Found");

        _mapper.Map(location, currentLocation);

        _database.SaveChanges();
    }
}