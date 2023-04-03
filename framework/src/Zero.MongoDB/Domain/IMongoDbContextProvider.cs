namespace Zero.MongoDB.Domain
{
    public interface IMongoDbContextProvider<TMongoDbContext>
        where TMongoDbContext : IZeroMongoDbContext
    {
        Task<TMongoDbContext> GetDbContextAsync(CancellationToken cancellationToken = default);
    }
}
