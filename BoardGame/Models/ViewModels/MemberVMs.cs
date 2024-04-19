using BoardGame.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BoardGame.Models.ViewModels
{
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

        [Required(ErrorMessage = "Email is required!")]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// Provides extension methods for the RegisterVM class.
    /// </summary>
    public static class RegisterVMExt
    {
        /// <summary>
        /// Converts a RegisterVM instance to a MemberRegisterDTO instance.
        /// </summary>
        /// <param name="vm">The RegisterVM instance to convert.</param>
        /// <returns>A new MemberRegisterDTO instance with corresponding data.</returns>
        public static MemberRegisterDTO ToMemberDTO(this RegisterVM vm)
        {
            return new MemberRegisterDTO()
            {
                Account = vm.Account,
                Name = vm.Name,
                Email = vm.Email,
            };
        }
    }
}
