using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopsByEndDateDepartedSpec : Specification<Stop>
{
    private readonly DateTime? _endDate;

    public StopsByEndDateDepartedSpec(DateTime? endDate) => _endDate = endDate;

    public override Expression<Func<Stop, bool>> BuildExpression()
    {
        if (_endDate == null) return ShowAll;

        return x => x.DateArrived.CompareTo(_endDate) < 0;
    }
}