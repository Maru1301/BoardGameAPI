using BoardGame.Models.EFModels;
using MongoDB.Bson;
using Utilities;

namespace BoardGame.Models.DTOs
{
    public class MemberDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ConfirmCode { get; set; } = string.Empty;
    }

    public class MemberRegisterDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string EncryptedPassword
        {
            get
            {
                string salt = HashUtility.GenerateSalt();
                string result = HashUtility.ToSHA256(this.Password!, salt);
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
                ConfirmCode = member.ConfirmCode,
            };
    }
}


