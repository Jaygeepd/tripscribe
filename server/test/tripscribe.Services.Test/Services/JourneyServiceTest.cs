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
        
        
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
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
    
    [Theory]
    [InlineData("title", "description")]
    [InlineData(null, null)]
    public void UpdateJourney_ValidDataEntered_MapperAndSaved(string title, string description)
    {
        // Arrange
        var journeyList = _fixture.Build<Journey>()
        .Without(x => x.AccountJourneys)
        .Without(x => x.JourneyReviews)
        .CreateMany();
        var journeyDTO = _mapper.Map<JourneyDTO>(journeyList.First());
        
        _database.Get<Journey>().Returns(journeyList.AsQueryable());
        
        var service = RetrieveService();

        var updateJourney = journeyList.First(); 
        
        // Act
        service.UpdateJourney(updateJourney.Id, journeyDTO);

        // Assert
        _database.Received(1).Get<Account>();
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Journey>(x => x.Title == updateJourney.Title));
    }
    
    [Fact]
    public void DeleteJourney_ValidIdEntered_MapperAndSaved()
    {
        // Arrange
        var journeyList = _fixture.Build<Journey>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .CreateMany();

        _database.Get<Journey>().Returns(journeyList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        service.DeleteJourney(journeyList.First().Id);

        // Assert
        _database.Received(1).Get<Journey>();
        _database.Received(1).SaveChanges();
    }

    [Fact]
    public void GetJourneyAccounts_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange
        const int journeyId = 1;
        const int accountId = 1;
        
        var journeyAccountList = _fixture.Build<AccountJourney>()
            .With(x => x.AccountId, accountId)
            .With(x => x.JourneyId, journeyId)
            .CreateMany()
            .ToList();

        var accountList = _fixture.Build<Account>()
            .Without(x => x.JourneyReviews)
            .With(x => x.Id, accountId)
            .With(x => x.AccountJourneys, journeyAccountList)
            .CreateMany();
        _database.Get<Account>().Returns(accountList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        var result = service.GetJourneyAccounts(journeyId);

        // Assert
        result.Should().BeEquivalentTo(accountList, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public void GetJourneyReviews_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange 
        var journeyList = _fixture.Build<Journey>()
            .Without(x => x.AccountJourneys)
            .Without(x => x.JourneyReviews)
            .CreateMany();
        _database.Get<Journey>().Returns(journeyList.AsQueryable());
        
        var reviewList = _fixture.Build<Review>()
            .Without(x => x.LocationReviews)
            .Without(x => x.JourneyReviews)
            .Without(x => x.StopReviews)
            .CreateMany();
        _database.Get<Review>().Returns(reviewList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetJourneyReviews(journeyList.First().Id);
        
        // Assert
        result.Should().BeEquivalentTo(reviewList, options => options.ExcludingMissingMembers());
    }

    private IJourneyService RetrieveService()
    {
        return new JourneyService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<JourneyProfile>();
            cfg.AddProfile<AccountProfile>();
            cfg.AddProfile<ReviewProfile>();
        });
        
        return new Mapper(config);
    }
}