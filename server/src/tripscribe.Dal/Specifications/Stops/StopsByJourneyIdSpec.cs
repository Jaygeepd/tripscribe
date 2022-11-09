using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopsByJourneyIdSpec : Specification<Stop>
{
    private readonly int _journeyId;
    
    public StopsByJourneyIdSpec(int id) => _journeyId = id;

    public override Expression<Func<Stop, bool>> BuildExpression()
    {
        return _journeyId <= 0 ? ShowNone : x => x.JourneyId == _journeyId;
    }
        
}