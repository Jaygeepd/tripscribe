using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByTypeSpec : Specification<Location>
{
    private readonly string? _type;
    
    public LocationsByTypeSpec(string? type) => _type = type;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_type)) return ShowAll;

        return x => x.Name.Contains(_type);
    }
        
}