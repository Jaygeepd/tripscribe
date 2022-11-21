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

public class JourneyServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;

    public JourneyServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
        _fixture = new Fixture();
    }

    [Fact]
    public void GetJourney_WhenJourneyExist_ReturnsJourney()
    {
        // Arrange
        const int id = 1;

        var journey = new Journey { Id = id };
        
        var journeys = new List<Journey> { journey };
        
        _database.Get<Journey>().Returns(journeys.AsQueryable());

        var service = RetrieveService();

        // Act
        var result = service.GetJourney(id);
        
        // Assert
        result.Should().BeEquivalentTo(journey, options => options.ExcludingMissingMembers());

    }
    
    [Fact]
    public void GetJourneys_WhenJourneysExist_ReturnsJourneyList()
    {
        // Arrange 
        var journeyList = _fixture.Build<Journey>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .CreateMany();
        _database.Get<Journey>().Returns(journeyList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetJourneys();
        
        // Assert
        result.Should().BeEquivalentTo(journeyList, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateJourney_ValidDataEntered_MapperAndSaved()
    {
        // Arrange 
        const int id = 1;
        const string title = "Test Title";
        const string description = "Test Description";
        DateTime timestamp = DateTime.Now;

        var newJourney = new JourneyDTO
        {
            Id = id,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };

        var service = RetrieveService();

        // Act
        service.CreateJourney(newJourney);
        
        // Assert
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Journey>(x => x.Title == newJourney.Title));
    }

    private IJourneyService RetrieveService()
    {
        return new JourneyService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<JourneyProfile>();
        });
        
        return new Mapper(config);
    }
}