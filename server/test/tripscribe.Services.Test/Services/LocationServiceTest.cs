using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using tripscribe.Services.Exceptions;
using tripscribe.Services.Profiles;
using tripscribe.Services.Services;

namespace tripscribe.Services.Test.Services;

public class LocationServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture; 

    public LocationServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
        _fixture = new Fixture();
        
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }

    [Fact]
    public void GetLocation_WhenLocationExist_ReturnsLocation()
    {
        // Arrange
        const int id = 1;

        var location = new Location { Id = id };
        
        var locations = new List<Location> { location };
        
        _database.Get<Location>().Returns(locations.AsQueryable());

        var service = RetrieveService();

        // Act
        var result = service.GetLocation(id);
        
        // Assert
        result.Should().BeEquivalentTo(location, options => options.ExcludingMissingMembers());

    }
    
    [Fact]
    public void GetLocations_WhenLocationsExist_ReturnsLocationList()
    {
        // Arrange 
        var locationList = _fixture.Build<Location>()
            .Without(x => x.LocationReviews)
            .Without(x => x.Stop)
            .CreateMany();
        _database.Get<Location>().Returns(locationList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetLocations();
        
        // Assert
        result.Should().BeEquivalentTo(locationList, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateLocation_ValidDataEntered_MapperAndSaved()
    {
        // Arrange 
        const int id = 1;
        const string name = "Test Name";
        const string locationType = "Attraction";
        DateTime dateVisited = DateTime.Now;

        var newLocation = new LocationDTO
        {
            Id = id,
            Name = name,
            LocationType = locationType,
            DateVisited = dateVisited
        };

        var service = RetrieveService();

        // Act
        service.CreateLocation(newLocation);
        
        // Assert
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Location>(x => x.Name == newLocation.Name));
    }

    [Fact]
    public void UpdateLocation_LocationDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var location = _fixture.Build<Location>()
            .With(x => x.Id, correctId)
            .Create();

        var updateDTO = _fixture.Build<LocationDTO>()
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.UpdateLocation(incorrectId, updateDTO));

    }
    
    [Fact]
    public void DeleteLocation_ValidIdEntered_MapperAndSaved()
    {
        // Arrange
        var locationList = _fixture.Build<Location>()
            .Without(x => x.LocationReviews)
            .CreateMany();

        _database.Get<Location>().Returns(locationList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        service.DeleteLocation(locationList.First().Id);

        // Assert
        _database.Received(1).Get<Location>();
        _database.Received(1).SaveChanges();
    }
    
    [Fact]
    public void DeleteLocation_LocationDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var location = _fixture.Build<Location>()
            .With(x => x.Id, correctId)
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.DeleteLocation(incorrectId));

    }

    private ILocationService RetrieveService()
    {
        return new LocationService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<LocationProfile>();
        });
        
        return new Mapper(config);
    }
}