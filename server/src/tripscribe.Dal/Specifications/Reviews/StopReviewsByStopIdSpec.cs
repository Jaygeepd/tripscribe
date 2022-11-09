using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class StopReviewsByStopIdSpec : Specification<StopReview>
{
    private readonly int _stopId;
    
    public StopReviewsByStopIdSpec(int id) => _stopId = id;

    public override Expression<Func<StopReview, bool>> BuildExpression()
    {
        if (_stopId <= 0) return ShowNone;

        return x => x.StopId == _stopId;
    }
        
}