using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Reviews;

public class ReviewByIdSpec : Specification<Review>
{
    private readonly int _id;
    
    public ReviewByIdSpec(int id) => _id = id;
    
    public override Expression<Func<Review, bool>> BuildExpression() =>
        x => x.Id == _id;
}