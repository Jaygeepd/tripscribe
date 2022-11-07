using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace tripscribe.Dal.Contexts;

[ExcludeFromCodeCoverage]
public abstract class BaseContext : DbContext
{
    private readonly string _connectionString;

    protected BaseContext(DbContextOptions option) : base(option)
    { }

    protected BaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }
        
    public IQueryable<T> Get<T>() where T : class
    {
        return Set<T>().AsQueryable();
    }
        
    public new T Add<T>(T item) where T : class
    {
        return Set<T>().Add(item).Entity;
    }

    public void Add<T>(params T[] items) where T : class
    {
        Set<T>().AddRange(items);
    }

    public void AddAsync<T>(params T[] items) where T : class
    {
        Set<T>().AddRangeAsync(items);
    }

    public void Delete<T>(params T[] items) where T : class
    {
        Set<T>().RemoveRange(items);
    }
        
    public bool ExecuteWithinTransaction(Action action)
    {
        using var transaction = Database.BeginTransaction();
        try
        {
            action();
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!string.IsNullOrWhiteSpace(_connectionString) && !optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}