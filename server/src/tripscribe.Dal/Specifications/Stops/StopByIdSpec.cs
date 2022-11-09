using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopByIdSpec : Specification<Stop>
{
    private readonly int _id;
    
    public StopByIdSpec(int id) => _id = id;
    
    public override Expression<Func<Stop, bool>> BuildExpression() =>
        x => x.Id == _id;
}