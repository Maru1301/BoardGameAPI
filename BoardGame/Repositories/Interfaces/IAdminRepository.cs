using BoardGame.Models.DTOs;

namespace BoardGame.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        public bool CheckAccountExist(string account);

        public Task<AdminDTO?> SearchByAccount(string account);

        public void AddAdmin(AdminCreateDTO dto);
    }
}
