using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using Utilities;

namespace BoardGame.Services
{
    public class AdminService(IUnitOfWork unitOfWork, JWTHelper jwt) : IService, IAdminService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly JWTHelper _jwt = jwt;

        public async Task<string> AddAdmin(AdminCreateDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (await CheckAccountExistAsync(dto.Account) == false) throw new AdminServiceException(ErrorCode.AccountExist);

                await _unitOfWork.Admins.AddAsync(dto.To<Admin>());

                await _unitOfWork.CommitTransactionAsync();
                return "Admin created successfully";
            }
            catch(AdminServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw; 
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var admin = await _unitOfWork.Admins.GetByAccountAsync(dto.Account);
            if (admin == null || !ValidatePassword(admin.To<AdminDTO>(), dto.Password))
            {
                throw new AdminServiceException(ErrorCode.InvalidAccountOrPassword);
            }

            // Authorize the user and generate a JWT token.
            var token = _jwt.GenerateToken(admin.Id, dto.Account, Role.Admin);

            return token;
        }

        private static bool ValidatePassword(AdminDTO admin, string password)
        {
            return HashUtility.ToSHA256(password, admin.Salt) == admin.EncryptedPassword;
        }

        private async Task<bool> CheckAccountExistAsync(string account)
        {
            var entity = await _unitOfWork.Admins.GetByAccountAsync(account);

            return entity != null;
        }
    }
    public class AdminServiceException(string ex) : Exception(ex)
    {
    }
}
