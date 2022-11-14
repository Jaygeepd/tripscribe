using AutoMapper;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Stops;
using tripscribe.Services.DTOs;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace tripscribe.Services.Services;

public class StopService
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    public StopService(ITripscribeDatabase database, IMapper mapper) =>
        (_database, _mapper) = (database, mapper);

    public IList<StopDTO> GetStops(int? id = null, string? name = null, DateTime? startArrivedTime = null, 
        DateTime? endArrivedTime = null, DateTime? startDepartedTime = null, DateTime? endDepartedTime = null, int? journeyId = null)
    {
        var stopQuery = _database
            .Get<Stop>()
            .Where(new StopSearchSpec(id, name, startArrivedTime, endArrivedTime, startDepartedTime, endDepartedTime, journeyId));

        return _mapper
            .ProjectTo<StopDTO>(stopQuery)
            .ToList();
        
    }

    public void UpdateStops(int id, StopDTO stop)
    {

        var currentStop = _database
            .Get<Stop>()
            .FirstOrDefault(new StopByIdSpec(id));

        if (currentStop == null) throw new Exception("Not Found");

        _mapper.Map(stop, currentStop);

        _database.SaveChanges();
    }

}