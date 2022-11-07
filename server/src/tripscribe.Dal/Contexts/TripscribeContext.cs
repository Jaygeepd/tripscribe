using Microsoft.EntityFrameworkCore;
using tripscribe.Dal.Interfaces;
using tripscribe.Dal.Models;

namespace tripscribe.Dal.Contexts;

public class TripscribeContext : BaseContext,ITripscribeDatabase
{
    public TripscribeContext(DbContextOptions option) : base(option) { }
    public TripscribeContext(string connectionString) : base(connectionString) { }
    
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Journey> Journeys { get; set; }
}