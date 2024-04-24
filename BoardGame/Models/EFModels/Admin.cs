using MongoDB.Bson;

namespace BoardGame.Models.EFModels
{
    public class Admin
    {
        public ObjectId Id { get; set; } 

        public string Account { get; set; } = string.Empty;

        public string EncryptedPassword { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;
    }
}
