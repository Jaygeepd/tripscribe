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
            LastName = "Bloggs"
        };

        Account account2 = new Account
        {
            Id = 2,
            Email = "tester2@gmail.com",
            FirstName = "Jane",
            LastName = "Doe"
        };

        Account account3 = new Account
        {
            Id = 3,
            Email = "tester3@yahoo.com",
            FirstName = "Alex",
            LastName = "Bell"
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

        AccountJourney accountJourney1 = new AccountJourney
        {
            Id = 1,
            AccountId = 1,
            JourneyId = 2
        };
        
        AccountJourney accountJourney2 = new AccountJourney
        {
            Id = 1,
            AccountId = 2,
            JourneyId = 3
        };
        
        AccountJourney accountJourney3 = new AccountJourney
        {
            Id = 1,
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

        database.SaveChanges();
    }
}