using System.Linq.Expressions;
using tripscribe.Dal.Models;
using tripscribe.Dal.Specifications.Accounts;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class JourneySearchSpec : Specification<Journey>
{
    private readonly Specification<Journey> _spec;

    public JourneySearchSpec(string? title, DateTime? startDate, DateTime? endDate)
    {
        _spec = new JourneyByTitleSpec(title)
            .Or(new JourneyByStartTimeSpec(startDate))
            .Or(new JourneyByEndTimeSpec(endDate));
    }

    public override Expression<Func<Journey, bool>> BuildExpression() => _spec.BuildExpression();

}