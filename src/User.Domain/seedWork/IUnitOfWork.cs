namespace User.Domain.seedWork;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntityAsync(CancellationToken cancellationToken);
}