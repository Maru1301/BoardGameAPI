using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BoardGame.ApiControllers.Models;

public class MemberResponse
{
    public string Name { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
}

public class MemberLoginRequest
{
    [Required(ErrorMessage = "Account is required!")]
    [StringLength(50)]
    public string Account { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required!")]
    [StringLength(50)]
    public string Password { get; set; } = string.Empty;
}

public class MemberLoginResponse
{
    public string Token { get; set; } = string.Empty;
}

public class RegisterRequest
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

public class RegisterResponse : ResponseBase
{

}

public class ResendConfirmationCodeResponse : ResponseBase
{

}

public class EditMemberInfoRequest : RequestBase
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}

public class EditMemberInfoResponse : ResponseBase
{
    
}

public class ResetPasswordRequest : RequestBase
{
    public string OldPassword { get; set; } = string.Empty;

    public string NewPassword { get; set; } = string.Empty;
}

public class DeleteMemberRequest
{
    public string? InputAccount { get; set; }
    public string JwtAccount { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class RequestBase
{
    public string MemberId { get; set; } = string.Empty;
}

public class ResponseBase
{
    public string Message { get; set; } = string.Empty;
}

