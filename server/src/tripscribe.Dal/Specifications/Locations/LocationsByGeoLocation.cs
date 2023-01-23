using System.Drawing;
using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationsByGeoLocation : Specification<Location>
{
    private readonly Point? _geoLocation;

    public LocationsByGeoLocation(Point? geoLocation) => _geoLocation = geoLocation;
    
    public override Expression<Func<Location, bool>> BuildExpression()
    {
        if (_geoLocation == null) return ShowAll;

        /// TODO Update with actual Geo search
        return ShowAll;
    }
}