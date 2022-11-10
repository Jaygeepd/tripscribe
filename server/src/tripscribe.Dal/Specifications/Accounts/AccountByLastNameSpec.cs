using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Accounts;

public class AccountByLastNameSpec : Specification<Account>
{
    private readonly string? _lastName;
    
    public AccountByLastNameSpec(string? lastName) => _lastName = lastName;

    public override Expression<Func<Account, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_lastName)) return ShowAll;
        
        return x => x.Email.Contains(_lastName);
    }
    
}