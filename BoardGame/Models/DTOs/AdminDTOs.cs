using Utilities;

namespace BoardGame.Models.DTOs
{
    public class AdminDTO
    {
        public string Account { get; set; } = string.Empty;

        public string EncryptedPassword { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;
    }

    public class AdminCreateDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = HashUtility.GenerateSalt();

        public string EncryptedPassword
        {
            get
            {
                string salt = Salt;
                string result = HashUtility.ToSHA256(this.Password, salt);
                return result;
            }
        }
    }
}
