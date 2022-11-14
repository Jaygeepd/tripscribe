using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Stops;

public class StopsByNameSpec : Specification<Stop>
{
    private readonly string? _name;
    
    public StopsByNameSpec(string? name) => _name = name;

    public override Expression<Func<Stop, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return ShowAll;

        return x => x.Name.Contains(_name);
    }
        
}