
namespace Application.Persistence.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		void BeginTransaction();
		Task BeginTransactionAsync(CancellationToken cancellationToken = default);
		void Commit();
		Task CommitAsync(CancellationToken cancellationToken = default);
		void Rollback();
		Task RollbackAsync(CancellationToken cancellationToken = default);
		void SaveAndCommit();
		Task SaveAndCommitAsync(CancellationToken cancellationToken = default);
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
