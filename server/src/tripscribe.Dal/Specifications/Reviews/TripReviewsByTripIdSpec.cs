using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class TripReviewsByTripIdSpec : Specification<TripReview>
{
    private readonly int _tripId;
    
    public TripReviewsByTripIdSpec(int id) => _tripId = id;

    public override Expression<Func<TripReview, bool>> BuildExpression()
    {
        if (_tripId <= 0) return ShowNone;

        return x => x.TripId == _tripId;
    }
        
}