using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByStartDateSpec : Specification<Location>
{
    private readonly DateTime? _startDate;

    public LocationsByStartDateSpec(DateTime? startDate) => _startDate = startDate;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (_startDate == null) return ShowAll;

        return x => x.DateVisited.CompareTo(_startDate) > 0;
    }
}