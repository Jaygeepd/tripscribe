using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByEndDateSpec : Specification<Location>
{
    private readonly DateTime? _endDate;

    public LocationsByEndDateSpec(DateTime? endDate) => _endDate = endDate;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (_endDate == null) return ShowAll;

        return x => x.DateVisited.CompareTo(_endDate) < 0;
    }
}