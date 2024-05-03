using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Services.Interfaces;
using Utilities;

namespace BoardGame.Services
{
    public class AdminService(IUnitOfWork unitOfWork) : IService, IAdminService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<string> AddAdmin(AdminCreateDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (_unitOfWork.Admins.CheckAccountExist(dto.Account))
                {
                    throw new AdminServiceException("Account already exists");
                }

                await _unitOfWork.Admins.AddAdminAsync(dto);

                await _unitOfWork.CommitTransactionAsync();
                return "Admin created successfully";
            }
            catch(AdminServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var admin = await _unitOfWork.Admins.SearchByAccountAsync(dto.Account);
            if (admin == null || !ValidatePassword(admin, dto.Password))
            {
                throw new AdminServiceException("Invalid Account or Password!");
            }

            return Roles.Admin;
        }

        private static bool ValidatePassword(AdminDTO admin, string password)
        {
            return HashUtility.ToSHA256(password, admin.Salt) == admin.EncryptedPassword;
        }
    }
    public class AdminServiceException(string ex) : Exception(ex)
    {
    }
}
