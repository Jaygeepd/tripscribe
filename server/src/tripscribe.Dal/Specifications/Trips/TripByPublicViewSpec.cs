using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Trips;

public class TripByPublicViewSpec : Specification<Trip>
{
    private readonly Boolean? _publicView;
    
    public TripByPublicViewSpec(Boolean publicView=false) => _publicView = _publicView;

    public override Expression<Func<Trip, bool>> BuildExpression()
    {
        return ShowAll;
        
        // TODO Implement the public/private retrieval spec
    }
}