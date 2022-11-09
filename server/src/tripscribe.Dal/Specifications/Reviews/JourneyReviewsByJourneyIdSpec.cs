using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class JourneyReviewsByJourneyIdSpec : Specification<JourneyReview>
{
    private readonly int _journeyId;
    
    public JourneyReviewsByJourneyIdSpec(int id) => _journeyId = id;

    public override Expression<Func<JourneyReview, bool>> BuildExpression()
    {
        if (_journeyId <= 0) return ShowNone;

        return x => x.JourneyId == _journeyId;
    }
        
}