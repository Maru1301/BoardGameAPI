using System.ComponentModel.DataAnnotations;

namespace BoardGame.Models.ViewModels
{
    public class AdminVMs 
    { 
        public class LoginVM
        {
            [Required(ErrorMessage = "Account is required!")]
            [StringLength(50)]
            public string Account { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required!")]
            [StringLength(50)]
            public string Password { get; set; } = string.Empty;
        }

        public class AdminCreateVM
        {

            [Required]
            [StringLength(30)]
            public string Account { get; set; } = string.Empty;

            [Required]
            [StringLength(70)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required]
            [StringLength(70)]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
    }
}
