using BoardGame.Repositories.Interfaces;
using BoardGame.Repositories;
using BoardGame.Models.EFModels;

namespace BoardGame.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private bool _disposed;

        public IMemberRepository Members { get; private set; }
        public IAdminRepository Admins { get; private set; }
        public IGameRepository Games { get; private set; }
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Members = new Lazy<MemberRepository>(() => new MemberRepository(_dbContext)).Value;
            Admins = new Lazy<AdminRepository>(() => new AdminRepository(_dbContext)).Value;
            Games = new Lazy<GameRepository>(() => new GameRepository(_dbContext)).Value;
        }

        public async Task BeginTransactionAsync()
        {
            // todo: MongoDb EntityFrameworkCore does not support Transaction yet
            //await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.SaveChangesAsync();
            // todo: MongoDb EntityFrameworkCore does not support Transaction yet
            //await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            // todo: MongoDb EntityFrameworkCore does not support Transaction yet
            //await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();

                    _disposed = true;
                }
            }
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository Members { get; }
        IAdminRepository Admins { get; }
        IGameRepository Games { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
