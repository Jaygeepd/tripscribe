using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class JourneyByIdSpec : Specification<Journey>
{
    private readonly int? _id;
    
    public JourneyByIdSpec(int? id) => _id = id;

    public override Expression<Func<Journey, bool>> BuildExpression()
    {
        if (_id == null) return ShowAll;
        
        return x => x.Id == _id;
    }
}