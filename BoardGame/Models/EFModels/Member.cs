using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace BoardGame.Models.EFModels
{
    [Collection("member")]
    public class Member
    {
        public ObjectId Id { get; set; }

        public string Account { get; set; } = string.Empty;

        public string EncryptedPassword { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ConfirmCode { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; }
    }
}
