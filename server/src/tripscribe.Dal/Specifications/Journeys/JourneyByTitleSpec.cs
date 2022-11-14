using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Journeys;

public class JourneyByTitleSpec: Specification<Journey>
{
    private readonly string? _title;
    
    public JourneyByTitleSpec(string? title) => _title = title;

    public override Expression<Func<Journey, bool>> BuildExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return ShowAll;
        
        return x => x.Title.Contains(_title);
    }
    
}