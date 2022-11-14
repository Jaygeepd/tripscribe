using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class JourneyByStartTimeSpec : Specification<Journey>
{
    private readonly DateTime? _startDate;

    public JourneyByStartTimeSpec(DateTime? startDate) => _startDate = startDate;

    public override Expression<Func<Journey, bool>> BuildExpression()
    {
        if (_startDate == null) return ShowAll;

        return x => x.Timestamp.CompareTo(_startDate) > 0;
    }
}