using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Trips;

public class TripByStartDateSpec : Specification<Trip>
{
    private readonly DateTime? _startDate;

    public TripByStartDateSpec(DateTime? startDate) => _startDate = startDate;

    public override Expression<Func<Trip, bool>> BuildExpression()
    {
        if (_startDate == null) return ShowAll;

        return x => x.Timestamp.CompareTo(_startDate) > 0;
    }
}