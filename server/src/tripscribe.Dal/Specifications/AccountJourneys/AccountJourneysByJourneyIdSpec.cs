using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.AccountJourneys;

public class AccountJourneysByJourneyIdSpec : Specification<AccountJourney>
{
    private readonly int _id;
    
    public AccountJourneysByJourneyIdSpec(int id) => _id = id;
    
    public override Expression<Func<AccountJourney, bool>> BuildExpression() =>
        x => x.JourneyId == _id;
}