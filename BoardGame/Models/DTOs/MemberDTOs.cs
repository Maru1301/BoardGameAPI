using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace BoardGame.Models.DTOs
{
    public class MemberDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string ConfirmCode { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; } = false;
    }

    public class LoginDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDTO
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

    public class EditDTO
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class ResetPasswordDTO
    {
        public ObjectId Id { get; set; }

        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string Salt { get; set; } = HashUtility.GenerateSalt();

        public string EncryptedPassword
        {
            get
            {
                string result = HashUtility.ToSHA256(this.NewPassword!, Salt);
                return result;
            }
        }
    }
}


