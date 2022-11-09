using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class LocationReviewsByLocationIdSpec : Specification<LocationReview>
{
    private readonly int _locationId;
    
    public LocationReviewsByLocationIdSpec(int id) => _locationId = id;

    public override Expression<Func<LocationReview, bool>> BuildExpression()
    {
        if (_locationId <= 0) return ShowNone;

        return x => x.LocationId == _locationId;
    }
        
}