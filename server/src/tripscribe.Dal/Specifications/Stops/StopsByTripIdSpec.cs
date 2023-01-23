using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopsByTripIdSpec : Specification<Stop>
{
    private readonly int? _tripId;
    
    public StopsByTripIdSpec(int? id) => _tripId = id;

    public override Expression<Func<Stop, bool>> BuildExpression()
    {
        if (_tripId == null) return ShowAll;

        return x => x.TripId == _tripId;
    }
        
}