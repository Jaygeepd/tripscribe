using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByStopIdSpec : Specification<Location>
{
    private readonly int? _stopId;
    
    public LocationsByStopIdSpec(int? id) => _stopId = id;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (_stopId == null) return ShowAll; 
        
        return x => x.StopId == _stopId;
    }
        
}