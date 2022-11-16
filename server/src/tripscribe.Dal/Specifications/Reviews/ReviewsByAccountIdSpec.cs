using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class ReviewsByAccountIdSpec : Specification<Review>
{
    private readonly int _id;
    
    public ReviewsByAccountIdSpec(int id)
    {
        _id = id;
    }
    
    public override Expression<Func<Review, bool>> BuildExpression()
    {
        return x => 
            x.JourneyReviews
                .Any(y => new JourneyReviewsByAccountIdSpec(_id)
                .Embed()(y)) || 
            x.StopReviews
                .Any(y => new StopReviewsByAccountIdSpec(_id)
                .Embed()(y)) || 
            x.LocationReviews
                .Any(y => new LocationReviewsByAccountIdSpec(_id)
                .Embed()(y));
    }
}