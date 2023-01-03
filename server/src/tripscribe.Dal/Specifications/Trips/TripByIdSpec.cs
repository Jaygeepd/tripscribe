using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Trips;

public class TripByIdSpec : Specification<Trip>
{
    private readonly int? _id;
    
    public TripByIdSpec(int? id) => _id = id;

    public override Expression<Func<Trip, bool>> BuildExpression()
    {
        if (_id == null) return ShowAll;
        
        return x => x.Id == _id;
    }
}