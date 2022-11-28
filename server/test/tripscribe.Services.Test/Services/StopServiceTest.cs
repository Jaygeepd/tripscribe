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
        
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
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
    
    
    [Theory]
    [InlineData("name", "2017-07-10", "2017-07-13")]
    [InlineData(null, null, null)]
    public void UpdateStop_ValidDataEntered_MapperAndSaved(string name, DateTime dateArrived, DateTime dateDeparted)
    {
        // Arrange
        var stopList = _fixture.Build<Stop>()
        .Without(x => x.StopReviews)
        .CreateMany();
        var stopDTO = _mapper.Map<StopDTO>(stopList.First());
        
        _database.Get<Stop>().Returns(stopList.AsQueryable());
        
        var service = RetrieveService();

        var updateStop = stopList.First(); 
        
        // Act
        service.UpdateStop(updateStop.Id, stopDTO);

        // Assert
        _database.Received(1).Get<Stop>();
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Stop>(x => x.Name == updateStop.Name));
    }
    
    [Fact]
    public void UpdateStop_StopDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var stop = _fixture.Build<Stop>()
            .With(x => x.Id, correctId)
            .Create();

        var updateDTO = _fixture.Build<StopDTO>()
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.UpdateStop(incorrectId, updateDTO));

    }
    
    [Fact]
    public void DeleteStop_ValidIdEntered_MapperAndSaved()
    {
        // Arrange
        var stopList = _fixture.Build<Stop>()
            .Without(x => x.StopReviews)
            .CreateMany();

        _database.Get<Stop>().Returns(stopList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        service.DeleteStop(stopList.First().Id);

        // Assert
        _database.Received(1).Get<Stop>();
        _database.Received(1).SaveChanges();
    }

    [Fact]
    public void GetStopReviews_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange 
        var stopList = _fixture.Build<Stop>()
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Stop>().Returns(stopList.AsQueryable());
        
        var reviewList = _fixture.Build<Review>()
            .Without(x => x.LocationReviews)
            .Without(x => x.JourneyReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Review>().Returns(reviewList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetStopReviews(stopList.First().Id);
        
        // Assert
        result.Should().BeEquivalentTo(reviewList, options => options.ExcludingMissingMembers());
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