using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Utility;

namespace BoardGame.Models.DTO;

public class MemberDTO
{
    public string Id { get; set;} = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string ConfirmCode { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; } = false;
}

public class MemberResponseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
}

public class LoginRequestDTO
{
    public string Account { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class RegisterRequestDTO
{
    public string Account { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Salt { get; set; } = HashUtility.GenerateSalt();

    public string EncryptedPassword
    {
        get
        {
            string result = HashUtility.ToSHA256(this.Password!, Salt);
            return result;
        }
    }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ConfirmCode { get; set; } = string.Empty;
}

public class EditRequestDTO
{
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class EditDTO
{
    public ObjectId Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordRequestDTO
{
    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}

public class ResetPasswordDTO
{
    public ObjectId Id { get; set; }

    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;

    public string Salt { get; set; } = HashUtility.GenerateSalt();

    public string EncryptedPassword => HashUtility.ToSHA256(this.NewPassword!, Salt);
}


