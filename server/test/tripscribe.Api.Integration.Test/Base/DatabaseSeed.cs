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

        Account account4 = new Account
        {
            Id = 4,
            Email = "friends@outlook.com",
            FirstName = "Joey",
            LastName = "Tribiani",
            Password = "fr13nds"
        };

        Account account5 = new Account
        {
            Id = 5,
            Email = "lbrown@gmail.com",
            FirstName = "Leah",
            LastName = "Brown",
            Password = "p455word"
        };

        Trip trip1 = new Trip
        {
            Id = 1,
            Title = "French Trip",
            Description = "Short adventure around Paris",
            Timestamp = DateTime.Now - TimeSpan.FromDays(7)
        };

        Trip trip2 = new Trip
        {
            Id = 2,
            Title = "Biking Across Japan",
            Description = "Chris Broad inspired week",
            Timestamp = DateTime.Now - TimeSpan.FromDays(120)
        };

        Trip trip3 = new Trip
        {
            Id = 3,
            Title = "Scandinavian Sojourn",
            Description = "Travels in Sweden and Norway",
            Timestamp = DateTime.Now - TimeSpan.FromDays(3)
        };

        Trip trip4 = new Trip
        {
            Id = 4,
            Title = "Road Trippin'",
            Description = "Drives through the lower 48",
            Timestamp = DateTime.Now
        };

        Stop stop1 = new Stop
        {
            Id = 1,
            Name = "Paris",
            DateArrived = DateTime.Now - TimeSpan.FromDays(6),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(5),
            TripId = 1
        };

        Stop stop2 = new Stop
        {
            Id = 2,
            Name = "Osaka",
            DateArrived = DateTime.Now - TimeSpan.FromDays(110),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(108),
            TripId = 2
        };

        Stop stop3 = new Stop
        {
            Id = 3,
            Name = "Oslo",
            DateArrived = DateTime.Now - TimeSpan.FromDays(2),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(1),
            TripId = 3
        };

        Stop stop4 = new Stop
        {
            Id = 4,
            Name = "Chicago",
            DateArrived = DateTime.Now - TimeSpan.FromDays(14),
            DateDeparted = DateTime.Now - TimeSpan.FromDays(10),
            TripId = 4
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

        Location location4 = new Location
        {
            Id = 4,
            Name = "Soldier Field Tour",
            DateVisited = DateTime.Now - TimeSpan.FromDays(12),
            LocationType = "Guided Tour",
            StopId = 4
        };

        AccountTrip accountTrip1 = new AccountTrip
        {
            Id = 1,
            AccountId = 1,
            TripId = 2
        };

        AccountTrip accountTrip2 = new AccountTrip
        {
            Id = 2,
            AccountId = 2,
            TripId = 3
        };

        AccountTrip accountTrip3 = new AccountTrip
        {
            Id = 3,
            AccountId = 3,
            TripId = 1
        };
        
        AccountTrip accountTrip4 = new AccountTrip
        {
            Id = 4,
            AccountId = 4,
            TripId = 4
        };

        database.Add(account1);
        database.Add(account2);
        database.Add(account3);
        database.Add(account4);
        database.Add(account5);

        database.Add(trip1);
        database.Add(trip2);
        database.Add(trip3);
        database.Add(trip4);

        database.Add(accountTrip1);
        database.Add(accountTrip2);
        database.Add(accountTrip3);
        database.Add(accountTrip4);

        database.Add(stop1);
        database.Add(stop2);
        database.Add(stop3);
        database.Add(stop4);

        database.Add(location1);
        database.Add(location2);
        database.Add(location3);
        database.Add(location4);
        
        database.SaveChanges();
    }
}