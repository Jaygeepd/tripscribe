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

public class TripServiceTest
{
    private readonly ITripscribeDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;

    public TripServiceTest()
    {
        _database = Substitute.For<ITripscribeDatabase>();
        _mapper = GetMapper();
        _fixture = new Fixture();
        
        
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }

    [Fact]
    public void GetTrip_WhenTripExist_ReturnsTrip()
    {
        // Arrange
        const int id = 1;

        var trip = new Trip { Id = id };
        
        var trips = new List<Trip> { trip };
        
        _database.Get<Trip>().Returns(trips.AsQueryable());

        var service = RetrieveService();

        // Act
        var result = service.GetTrip(id);
        
        // Assert
        result.Should().BeEquivalentTo(trip, options => options.ExcludingMissingMembers());

    }
    
    [Fact]
    public void GetTrip_WhenTripExist_ReturnsTripList()
    {
        // Arrange 
        var tripList = _fixture.Build<Trip>()
            .Without(x => x.AccountTrips)
            .Without(x => x.TripReviews)
            .CreateMany();
        _database.Get<Trip>().Returns(tripList.AsQueryable());

        var service = RetrieveService();
        
        // Act 
        var result = service.GetTrips();
        
        // Assert
        result.Should().BeEquivalentTo(tripList, options => options.ExcludingMissingMembers());
    }

    [Fact]
    public void CreateTrip_ValidDataEntered_MapperAndSaved()
    {
        // Arrange 
        const int id = 1;
        const string title = "Test Title";
        const string description = "Test Description";
        DateTime timestamp = DateTime.Now;

        var newTrip = new TripDTO
        {
            Id = id,
            Title = title,
            Description = description,
            Timestamp = timestamp
        };

        var service = RetrieveService();

        // Act
        service.CreateTrip(newTrip);
        
        // Assert
        _database.Received(1).SaveChanges();
        _database.Received(1).Add(Arg.Is<Trip>(x => x.Title == newTrip.Title));
    }
    
    
    [Fact]
    public void UpdateTrip_TripDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var account = _fixture.Build<Trip>()
            .With(x => x.Id, correctId)
            .Create();

        var updateDTO = _fixture.Build<TripDTO>()
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.UpdateTrip(incorrectId, updateDTO));

    }
    
    [Fact]
    public void DeleteTrip_ValidIdEntered_MapperAndSaved()
    {
        // Arrange
        var tripList = _fixture.Build<Trip>()
            .Without(x => x.AccountTrips)
            .Without(x => x.TripReviews)
            .CreateMany();

        _database.Get<Trip>().Returns(tripList.AsQueryable());
        
        var service = RetrieveService();

        // Act
        service.DeleteTrip(tripList.First().Id);

        // Assert
        _database.Received(1).Get<Trip>();
        _database.Received(1).SaveChanges();
    }
    
    [Fact]
    public void DeleteTrip_TripDoesNotExist_ThrowNotFoundException()
    {
        // Arrange 
        const int correctId = 1;
        const int incorrectId = 2;
        
        var journey = _fixture.Build<Trip>()
            .With(x => x.Id, correctId)
            .Create();

        var service = RetrieveService();
        
        // Act/Assert
        Assert.Throws<NotFoundException>(() => service.DeleteTrip(incorrectId));

    }

    [Fact]
    public void GetTripAccounts_ValidIdEntered_ReturnedAndMapped()
    {
        // Arrange
        const int tripId = 1;
        const int accountId = 1;
        
        var account = _fixture
            .Build<Account>()
            .With(x => x.Id, accountId)
            .Without(x => x.AccountTrips)
            .CreateMany(1)
            .ToArray();

        var  tripIds = _fixture.MockWithOne(accountId);
        
        var accountJourneyList = _fixture
            .Build<AccountTrip>()
            .With(x => x.TripId, tripIds.GetValue)
            .With(x => x.AccountId, accountId)
            .With(x => x.Account, account.First())
            .CreateMany()
            .AsQueryable();

        _database.Get<AccountTrip>().Returns(accountJourneyList);
        
        var service = RetrieveService();

        // Act
        var result = service.GetTripAccounts(tripId);

        // Assert
        result.Should().BeEquivalentTo(account, options => options.ExcludingMissingMembers());
    }

    private ITripService RetrieveService()
    {
        return new TripService(_database, _mapper);
    }
    
    private static IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<TripProfile>();
            cfg.AddProfile<AccountProfile>();
            cfg.AddProfile<ReviewProfile>();
        });
        
        return new Mapper(config);
    }
}