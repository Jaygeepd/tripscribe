using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopsByStartDateArrivedSpec : Specification<Stop>
{
    private readonly DateTime? _startDate;

    public StopsByStartDateArrivedSpec(DateTime? startDate) => _startDate = startDate;

    public override Expression<Func<Stop, bool>> BuildExpression()
    {
        if (_startDate == null) return ShowAll;

        return x => x.DateArrived.CompareTo(_startDate) > 0;
    }
}