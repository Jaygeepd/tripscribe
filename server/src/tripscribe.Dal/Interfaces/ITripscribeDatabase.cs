namespace tripscribe.Dal.Interfaces;

public interface ITripscribeDatabase
{
    IQueryable<T> Get<T>() where T : class;
    T Add<T>(T item) where T : class;
    void Add<T>(params T[] items) where T : class;
    void AddAsync<T>(params T[] items) where T : class;
    void Delete<T>(params T[] items) where T : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    bool ExecuteWithinTransaction(Action action);
}