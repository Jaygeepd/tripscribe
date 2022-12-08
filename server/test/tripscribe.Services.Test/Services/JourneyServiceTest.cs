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
using tripscribe.Services.Test.Extensions;

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
    
    
    [Fact]
    public void UpdateJourney_JourneyDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var account = _fixture.Build<Journey>()
            .With(x => x.Id, correctId)
            .Create();

        var updateDTO = _fixture.Build<JourneyDTO>()
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.UpdateJourney(incorrectId, updateDTO));

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
    public void DeleteJourney_JourneyDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var journey = _fixture.Build<Journey>()
            .With(x => x.Id, correctId)
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.DeleteJourney(incorrectId));

    }

    [Fact]
    public void GetJourneyAccounts_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange
        const int journeyId = 1;
        const int accountId = 1;
        
        var account = _fixture
            .Build<Account>()
            .With(x => x.Id, accountId)
            .Without(x => x.AccountJourneys)
            .CreateMany(1)
            .ToArray();

        var  journeyIds = _fixture.MockWithOne(accountId);
        
        var accountJourneyList = _fixture
            .Build<AccountJourney>()
            .With(x => x.JourneyId, journeyIds.GetValue)
            .With(x => x.AccountId, accountId)
            .With(x => x.Account, account.First())
            .CreateMany()
            .AsQueryable();

        _database.Get<AccountJourney>().Returns(accountJourneyList);
        
        var service = RetrieveService();

        // Act
        var result = service.GetJourneyAccounts(journeyId);

        // Assert
        result.Should().BeEquivalentTo(account, options => options.ExcludingMissingMembers());
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