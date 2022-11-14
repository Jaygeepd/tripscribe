using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByNameSpec : Specification<Location>
{
    private readonly string? _name;
    
    public LocationsByNameSpec(string? name) => _name = name;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return ShowAll;

        return x => x.Name.Contains(_name);
    }
        
}