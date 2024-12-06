using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace BoardGame.Models.EFModels
{
    [Collection("admin")]
    public class Admin
    {
        public ObjectId Id { get; set; } 

        public string Account { get; set; } = string.Empty;

        public string EncryptedPassword { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;
    }
}
