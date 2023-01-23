using System.Drawing;
using System.Linq.Expressions;
using tripscribe.Dal.Models;
using Unosquare.EntityFramework.Specification.Common.Extensions;
using Unosquare.EntityFramework.Specification.Common.Primitive;

namespace tripscribe.Dal.Specifications.Locations;

public class LocationSearchSpec : Specification<Location>
{
    private readonly Specification<Location> _spec;

    public LocationSearchSpec(string? name, string? locType, DateTime? startDate, DateTime? endDate, Point? geoLocation, int? stopId)
    {
        _spec = new LocationsByNameSpec(name)
            .Or(new LocationsByTypeSpec(locType))
            .Or(new LocationsByStartDateSpec(startDate))
            .Or(new LocationsByEndDateSpec(endDate))
            .Or(new LocationsByStopIdSpec(stopId))
            .Or(new LocationsByGeoLocation(geoLocation));
    }

    public override Expression<Func<Location, bool>> BuildExpression() => _spec.BuildExpression();

}