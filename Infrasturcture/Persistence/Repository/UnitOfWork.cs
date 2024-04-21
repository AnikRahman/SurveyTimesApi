using Application.Persistence.Repository;
using Infrasturcture.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrasturcture.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (_transaction != null) return;

            _transaction = _context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null) return;

            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;

            await _transaction.CommitAsync(cancellationToken);
            _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync(cancellationToken);
            _transaction.Dispose();
            _transaction = null;
        }

        public void SaveAndCommit()
        {
            // Save changes and commit the active transaction
            _context.SaveChanges();
            _context.Database.CommitTransaction();
        }

        public async Task SaveAndCommitAsync(CancellationToken cancellationToken = default)
        {
            // Save changes and commit the active transaction asynchronously
            await _context.SaveChangesAsync(cancellationToken);
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            if (_transaction == null) return;

            _transaction.Dispose();
            _transaction = null;
        }
    }
}
