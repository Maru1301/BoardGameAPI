using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Repositories.Interfaces;
using BoardGame.Services.Interfaces;
using Utilities;

namespace BoardGame.Services
{
    public class AdminService : IService, IAdminService
    {
        private IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        public AdminService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }
        public void AddAdmin(AdminCreateDTO dto)
        {
            if (_adminRepository.CheckAccountExist(dto.Account))
            {
                throw new AdminServiceException("Account already exists");
            }

            _adminRepository.AddAdmin(dto);
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var admin = await _adminRepository.SearchByAccount(dto.Account);
            if (admin == null || !ValidatePassword(admin, dto.Password))
            {
                return string.Empty;
            }

            return Roles.Admin;
        }

        private static bool ValidatePassword(AdminDTO admin, string password)
        {
            return HashUtility.ToSHA256(password, admin.Salt) == admin.EncryptedPassword;
        }
    }
    public class AdminServiceException : Exception
    {
        public AdminServiceException(string ex) : base(ex)
        {
            
        }
    }
}
