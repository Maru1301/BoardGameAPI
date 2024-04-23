using AutoMapper;
using BoardGame.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BoardGame.Models.ViewModels
{
    public class MemberVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
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
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Account is required!")]
        [StringLength(50)]
        public string Account { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPassword is required!")]
        [StringLength(50)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
    }
}
