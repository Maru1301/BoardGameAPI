using System.ComponentModel.DataAnnotations;

namespace BoardGame.Models.EFModels
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        public string Account { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
