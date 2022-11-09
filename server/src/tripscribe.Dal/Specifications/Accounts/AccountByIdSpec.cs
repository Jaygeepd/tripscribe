using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Accounts;

public class AccountByIdSpec : Specification<Account>
{
    private readonly int _id;
    
    public AccountByIdSpec(int id) => _id = id;
    
    public override Expression<Func<Account, bool>> BuildExpression() =>
        x => x.Id == _id;
}