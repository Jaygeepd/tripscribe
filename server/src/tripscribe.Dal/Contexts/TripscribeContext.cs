using Microsoft.EntityFrameworkCore;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;

namespace tripscribe.Dal.Contexts;

public class TripscribeContext : BaseContext,ITripscribeDatabase
{
    public TripscribeContext(DbContextOptions option) : base(option) { }
    public TripscribeContext(string connectionString) : base(connectionString) { }
    
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Trip> Trips { get; set; }
    public virtual DbSet<Stop> Stops { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<AccountTrip> AccountTrips { get; set; }

}