using BoardGame.Models.EFModels;

namespace BoardGame.Repositories.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        public Task<Member?> GetByAccountAsync(string account);
        public Task<Member?> GetByNameAsync(string name);
        public Task<Member?> GetByEmailAsync(string email);
    }
}
