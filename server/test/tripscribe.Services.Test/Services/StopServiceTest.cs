using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;
using tripscribe.Services.DTOs;
using tripscribe.Services.Profiles;
using tripscribe.Services.Services;

namespace tripscribe.Services.Test.Services;

public class StopServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;

    public StopServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
        _fixture = new Fixture();
    }

    [Fact]
    public void GetStop_WhenStopExist_ReturnsStop()
    {
        // Arrange
        const int id = 1;

        var stop = new Stop { Id = id };
        
        var stops = new List<Stop> { stop };
        
        _database.Get<Stop>().Returns(stops.AsQueryable());

        var service = RetrieveService();

        // Act
        var result = service.GetStop(id);
        
        // Assert
        result.Should().BeEquivalentTo(stop, options => options.ExcludingMissingMembers());

    }
    
    [Fact]
    public void GetStops_WhenStopsExist_ReturnsStopList()
    {
        // Arrange 
        var stopList = _fixture.Build<Stop>()
            .Without(x => x.Locations)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Stop>().Returns(stopList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetStops();
        
        // Assert
        result.Should().BeEquivalentTo(stopList, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateStop_ValidDataEntered_MapperAndSaved()
    {
        // Arrange 
        const int id = 1;
        const string name = "Test Stop";
        DateTime dateArrived = DateTime.Now - TimeSpan.FromDays(1);
        DateTime dateDeparted = DateTime.Now;

        var newStop = new StopDTO
        {
            Id = id,
            Name = name,
            DateArrived = dateArrived,
            DateDeparted = dateDeparted
        };

        var service = RetrieveService();

        // Act
        service.CreateStop(newStop);
        
        // Assert
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Stop>(x => x.Name == newStop.Name));
    }
    
    

    private IStopService RetrieveService()
    {
        return new StopService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<StopProfile>();
        });
        
        return new Mapper(config);
    }
}