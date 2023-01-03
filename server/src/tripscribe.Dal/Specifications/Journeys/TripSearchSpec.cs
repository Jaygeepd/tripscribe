using System.Linq.Expressions;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class TripSearchSpec : Specification<Trip>
{
    private readonly Specification<Trip> _spec;

    public TripSearchSpec(string? title, DateTime? startDate, DateTime? endDate)
    {
        _spec = new TripByTitleSpec(title)
            .Or(new TripByStartDateSpec(startDate))
            .Or(new TripByEndDateSpec(endDate));
    }

    public override Expression<Func<Trip, bool>> BuildExpression() => _spec.BuildExpression();

}