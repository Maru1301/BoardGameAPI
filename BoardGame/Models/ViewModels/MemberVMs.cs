using System.ComponentModel.DataAnnotations;

namespace BoardGame.Models.ViewModels
{
    public class MemberVMs
    {
        public class MemberVM
        {
            public string Name { get; set; } = string.Empty;
            public string Account { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool IsConfirmed { get; set; }
        }

        public class LoginVM
        {
            [Required(ErrorMessage = "Account is required!")]
            [StringLength(50)]
            public string Account { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required!")]
            [StringLength(50)]
            public string Password { get; set; } = string.Empty;
        }

        public class RegisterVM
        {
            [Required]
            [StringLength(50)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [StringLength(50)]
            public string Account { get; set; } = string.Empty;

            [Required]
            [StringLength(50)]
            public string Password { get; set; } = string.Empty;

            [Required]
            [StringLength(50)]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; } = string.Empty;

            [EmailAddress]
            [Required]
            public string Email { get; set; } = string.Empty;
        }

        public class EditVM
        {
            public string Name { get; set; } = string.Empty;

            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        }

        public class ResetPasswordVM
        {
            public string OldPassword { get; set; } = string.Empty;

            public string NewPassword { get; set; } = string.Empty;
        }
    }
}
