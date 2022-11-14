using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Accounts;

public class AccountSearchSpec : Specification<Account>
{
    private readonly Specification<Account> _spec;

    public AccountSearchSpec(int? id, string? email, string? firstName, string? lastName)
    {
        _spec = new AccountByIdSpec(id)
            .Or(new AccountByEmailSpec(email))
            .Or(new AccountByFirstNameSpec(firstName)
                .Or(new AccountByLastNameSpec(lastName)));
    }

    public override Expression<Func<Account, bool>> BuildExpression() => _spec.BuildExpression();

}