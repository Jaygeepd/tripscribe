using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class StopReviewsByAccountIdSpec : Specification<StopReview>
{
    private readonly int _accountId;
    
    public StopReviewsByAccountIdSpec(int id) => _accountId = id;

    public override Expression<Func<StopReview, bool>> BuildExpression()
    {
        if (_accountId <= 0) return ShowNone;

        return x => x.AccountId == _accountId;
    }
        
}