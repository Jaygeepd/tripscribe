using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopSearchSpec : Specification<Stop>
{
    private readonly Specification<Stop> _spec;

    public StopSearchSpec(string? name, DateTime? arrivedStartDate, DateTime? arrivedEndDate, DateTime? departedStartDate, DateTime? departedEndDate, int? journeyId)
    {
        _spec = new StopsByNameSpec(name)
            .Or(new StopsByStartDateArrivedSpec(arrivedStartDate))
            .Or(new StopsByEndDateArrivedSpec(arrivedEndDate))
            .Or(new StopsByStartDateDepartedSpec(departedStartDate))
            .Or(new StopsByEndDateDepartedSpec(departedEndDate))
            .Or(new StopsByJourneyIdSpec(journeyId));
    }

    public override Expression<Func<Stop, bool>> BuildExpression() => _spec.BuildExpression();

}