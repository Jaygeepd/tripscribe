using tripscribe.Dal.Contexts;
using tripscribe.Dal.Models;

namespace Tripscribe.Api.Integration.Test.Base;

public static class DatabaseSeed
{
    public static void SeedDatabase(TripscribeContext database)
    {
        Account account1 = new Account
        {
            Id = 1,
            Email = "tester1@gmail.com",
            FirstName = "Joe",
            LastName = "Bloggs",
            Password = "password1"
        };

        Account account2 = new Account
        {
            Id = 2,
            Email = "tester2@gmail.com",
            FirstName = "Jane",
            LastName = "Doe",
            Password = "password2"
        };

        Account account3 = new Account
        {
            Id = 3,
            Email = "tester3@yahoo.com",
            FirstName = "Alex",
            LastName = "Bell",
            Password = "password3"
        };

        Journey journey1 = new Journey
        {
            Id = 1,
            Title = "French Trip",
            Description = "Short adventure around Paris",
            Timestamp = DateTime.Now - TimeSpan.FromDays(7)
        };

        Journey journey2 = new Journey
        {
            Id = 2,
            Title = "Biking Across Japan",
            Description = "Chris Broad inspired week",
            Timestamp = DateTime.Now - TimeSpan.FromDays(120)
        };

        Journey journey3 = new Journey
        {
            Id = 3,
            Title = "Scandinavian Sojourn",
            Description = "Travels in Sweden and Norway",
            Timestamp = DateTime.Now - TimeSpan.FromDays(3)
        };

        Stop stop1 = new Stop
        {
            Id = 1,
            Name = "Paris",
            DateArrived = DateTime.Now - TimeSpan.FromDays(6),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(5),
            JourneyId = 1
        };

        Stop stop2 = new Stop
        {
            Id = 2,
            Name = "Osaka",
            DateArrived = DateTime.Now - TimeSpan.FromDays(110),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(108),
            JourneyId = 2
        };

        Stop stop3 = new Stop
        {
            Id = 3,
            Name = "Oslo",
            DateArrived = DateTime.Now - TimeSpan.FromDays(2),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(1),
            JourneyId = 3
        };

        Location location1 = new Location
        {
            Id = 1,
            Name = "Eiffel Tower",
            DateVisited = DateTime.Now - TimeSpan.FromDays(6),
            LocationType = "Tourist Spot",
            StopId = 1
        };

        Location location2 = new Location
        {
            Id = 2,
            Name = "Honshu Club",
            DateVisited = DateTime.Now - TimeSpan.FromDays(109),
            LocationType = "Bar/Restaurant",
            StopId = 2
        };

        Location location3 = new Location
        {
            Id = 3,
            Name = "Fjord Boat Tour",
            DateVisited = DateTime.Now - TimeSpan.FromDays(2),
            LocationType = "Guided Tour",
            StopId = 3
        };

        AccountJourney accountJourney1 = new AccountJourney
        {
            Id = 1,
            AccountId = 1,
            JourneyId = 2
        };

        AccountJourney accountJourney2 = new AccountJourney
        {
            Id = 2,
            AccountId = 2,
            JourneyId = 3
        };

        AccountJourney accountJourney3 = new AccountJourney
        {
            Id = 3,
            AccountId = 3,
            JourneyId = 1
        };

        database.Add(account1);
        database.Add(account2);
        database.Add(account3);

        database.Add(journey1);
        database.Add(journey2);
        database.Add(journey3);

        database.Add(accountJourney1);
        database.Add(accountJourney2);
        database.Add(accountJourney3);

        database.Add(stop1);
        database.Add(stop2);
        database.Add(stop3);

        database.Add(location1);
        database.Add(location2);
        database.Add(location3);

        database.SaveChanges();
    }
}