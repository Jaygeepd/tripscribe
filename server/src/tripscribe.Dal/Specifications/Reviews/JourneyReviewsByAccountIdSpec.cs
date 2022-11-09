using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class JourneyReviewsByAccountIdSpec : Specification<JourneyReview>
{
    private readonly int _accountId;
    
    public JourneyReviewsByAccountIdSpec(int id) => _accountId = id;

    public override Expression<Func<JourneyReview, bool>> BuildExpression()
    {
        if (_accountId <= 0) return ShowNone;

        return x => x.AccountId == _accountId;
    }
        
}