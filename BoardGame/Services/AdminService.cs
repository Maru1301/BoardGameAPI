using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Repositories;
using BoardGame.Repositories.Interfaces;
using BoardGame.Services.Interfaces;
using Utilities;

namespace BoardGame.Services
{
    public class AdminService : IService, IAdminService
    {
        private IAdminRepository _repository;
        private readonly IConfiguration _configuration;
        public AdminService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _repository = adminRepository;
            _configuration = configuration;
        }
        public string AddAdmin(AdminCreateDTO dto)
        {
            using var session = _repository.GetMongoClient().StartSession();
            try
            {
                session.StartTransaction();
                if (_repository.CheckAccountExist(dto.Account))
                {
                    throw new AdminServiceException("Account already exists");
                }

                _repository.AddAdmin(dto);

                session.CommitTransaction();
                return "Admin created successfully";
            }
            catch (Exception)
            {
                session.AbortTransaction(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var admin = await _repository.SearchByAccount(dto.Account);
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
