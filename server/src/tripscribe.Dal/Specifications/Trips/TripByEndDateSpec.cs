using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Trips;

public class TripByEndDateSpec : Specification<Trip>
{
    private readonly DateTime? _endDate;

    public TripByEndDateSpec(DateTime? endDate) => _endDate = endDate;

    public override Expression<Func<Trip, bool>> BuildExpression()
    {
        if (_endDate == null) return ShowAll;

        return x => x.Timestamp.CompareTo(_endDate) < 0;
    }
}