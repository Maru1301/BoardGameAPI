using BoardGame.Infrastractures;
using BoardGame.Models.DTO;
using BoardGame.Models.EFModels;
using BoardGame.Services.Interfaces;
using FluentResults;
using Utility;

namespace BoardGame.Services
{
    public class AdminService(IUnitOfWork unitOfWork, JWTHelper jwt) : IService, IAdminService
    {
        private readonly JWTHelper _jwt = jwt;

        public async Task<string> AddAdmin(AdminCreateDTO dto)
        {
            await unitOfWork.BeginTransactionAsync();
            try
            {
                if (await CheckAccountExistAsync(dto.Account) == false)
                {
                    throw new AdminServiceException(ErrorCode.AccountExist);
                }

                await unitOfWork.Admins.AddAsync(dto.To<Admin>());

                await unitOfWork.CommitTransactionAsync();
                return "Admin created successfully";
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<Result<string>> ValidateUser(LoginRequestDTO dto)
        {
            var admin = await unitOfWork.Admins.GetByAccountAsync(dto.Account);
            if (admin == null || !ValidatePassword(admin.To<AdminDTO>(), dto.Password))
            {
                throw new AdminServiceException(ErrorCode.InvalidAccountOrPassword);
            }

            // Authorize the user and generate a JWT token.
            var result = _jwt.GenerateToken(admin.Id, dto.Account, Role.Admin);

            return result;
        }

        private static bool ValidatePassword(AdminDTO admin, string password)
        {
            return Utility.HashUtility.ToSHA256(password, admin.Salt) == admin.EncryptedPassword;
        }

        private async Task<bool> CheckAccountExistAsync(string account)
        {
            var entity = await unitOfWork.Admins.GetByAccountAsync(account);

            return entity == null;
        }
    }
    public class AdminServiceException(string ex) : Exception(ex)
    {
    }
}
