using BoardGame.Models.EFModels;

namespace BoardGame.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<Admin?> GetByAccountAsync(string account);
    }
}
