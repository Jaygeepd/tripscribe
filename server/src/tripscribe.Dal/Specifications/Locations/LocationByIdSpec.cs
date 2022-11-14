using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationByIdSpec : Specification<Location>
{
    private readonly int? _id;
    
    public LocationByIdSpec(int? id) => _id = id;

    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (_id == null) return ShowAll;

        return x => x.Id.Equals(_id);
    }
        
}