using System.ComponentModel.DataAnnotations;
using Utility;

namespace BoardGame.Models.DTO;

public class AdminLoginRequestDTO
{
    [Required(ErrorMessage = "Account is required!")]
    [StringLength(50)]
    public string Account { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required!")]
    [StringLength(50)]
    public string Password { get; set; } = string.Empty;
}

public class AdminDTO
{
    public string Account { get; set; } = string.Empty;

    public string EncryptedPassword { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;
}

public class AdminCreateRequestDTO
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
