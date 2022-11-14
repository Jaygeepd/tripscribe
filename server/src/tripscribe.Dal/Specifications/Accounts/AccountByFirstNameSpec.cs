using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Accounts;

public class AccountByFirstNameSpec : Specification<Account>
{
    private readonly string? _firstName;
    
    public AccountByFirstNameSpec(string? firstName) => _firstName = firstName;

    public override Expression<Func<Account, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_firstName)) return ShowAll;
        
        return x => x.FirstName.Contains(_firstName);
    }
    
}