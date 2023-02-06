using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.AccountJourneys;

public class AccountJourneysByAccountIdSpec : Specification<AccountTrip>
{
    private readonly int _id;
    
    public AccountJourneysByAccountIdSpec(int id) => _id = id;
    
    public override Expression<Func<AccountTrip, bool>> BuildExpression() =>
        x => x.AccountId == _id;
}