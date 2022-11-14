using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class JourneyByEndTimeSpec : Specification<Journey>
{
    private readonly DateTime? _endDate;

    public JourneyByEndTimeSpec(DateTime? endDate) => _endDate = endDate;

    public override Expression<Func<Journey, bool>> BuildExpression()
    {
        if (_endDate == null) return ShowAll;

        return x => x.Timestamp.CompareTo(_endDate) < 0;
    }
}