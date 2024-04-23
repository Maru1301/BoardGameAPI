using BoardGame.Models.EFModels;
using Utilities;

namespace BoardGame.Models.DTOs
{
    public class MemberDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string ConfirmCode { get; set; } = string.Empty;
    }

    public class MemberLoginDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class MemberRegisterDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = HashUtility.GenerateSalt();

        public string EncryptedPassword
        {
            get
            {
                string result = HashUtility.ToSHA256(this.Password!, Salt);
                return result;
            }
        }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ConfirmCode { get; set; } = string.Empty;
    }

    public static class MemberDTOExt
    {
        public static MemberDTO ToDTO(this Member member)
            => new()
            {
                Id = member.Id.ToString(),
                EncryptedPassword = member.EncryptedPassword,
                Salt = member.Salt,
                ConfirmCode = member.ConfirmCode,
            };
    }
}


