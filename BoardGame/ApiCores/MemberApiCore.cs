using BoardGame.ApiControllers.Models;
using BoardGame.Infrastractures;
using BoardGame.Models.DTO;
using BoardGame.Services.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Utility;

namespace BoardGame.ApiCores
{
    public class MemberApiCore(IMemberService memberService, ILogger<MemberApiCore> logger)
    {
        private readonly IMemberService _memberService = memberService;
        //private readonly ILogger<MemberApiCore> _logger = logger;

        public async Task<Result<IEnumerable<MemberResponse>>> ListMembers()
        {
            var members = await _memberService.ListMembers();

            return Result.Ok(members.Select(x => x.To<MemberResponse>()));
        }

        public async Task<Result<MemberResponse>> GetMemberInfo(string id)
        {
            var member = await _memberService.GetMemberInfo(new ObjectId(id));

            return Result.Ok(member.To<MemberResponse>());
        }

        public async Task<Result<MemberLoginResponse>> Login([FromBody] MemberLoginRequest login)
        {
            var result = await _memberService.ValidateUser(login.To<LoginRequestDTO>());

            return Result.Ok(new MemberLoginResponse{ Token = result.Value });
        }

        public async Task<Result<RegisterResponse>> Register(RegisterRequest vm)
        {
            var result = await _memberService.Register(vm.To<RegisterRequestDTO>());
            
            return Result.Ok(new RegisterResponse { Message = result.Value });
        }

        public async Task<Result<ResendConfirmationCodeResponse>> ResendConfirmationCode(string memberId)
        {
            var result = await _memberService.ResendConfirmationCode(new ObjectId(memberId));

            return Result.Ok(new ResendConfirmationCodeResponse { Message = result.Value });
        }

        public async Task<Result<EditMemberInfoResponse>> EditMemberInfo(EditMemberInfoRequest req)
        {
            var result = await _memberService.EditMemberInfo(req.To<EditDTO>());

            return Result.Ok(new EditMemberInfoResponse { Message = result.Value });
        }

        public async Task<Result<string>> ResetPassword(ResetPasswordRequest req)
        {
            var result = await _memberService.ResetPassword(req.To<ResetPasswordDTO>());

            if (result.IsFailed)
            {
                return result.WithValue("Reset failed.");
            }

            return Result.Ok(result.Value);
        }

        public async Task<Result<string>> ValidateEmail(string memberId, string confirmationCode)
        {
            var result = await _memberService.ValidateEmail(memberId, confirmationCode);

            if (result.IsFailed)
            {
                return result.WithValue("Validation failed.");
            }

            return Result.Ok(result.Value);
        }

        public async Task<Result<bool>> DeleteMember(DeleteMemberRequest req)
        {
            var account = req.JwtAccount;

            if (req.Role == Role.Admin.ToString())
            {
                if (string.IsNullOrEmpty(req.InputAccount))
                {
                    return Result.Fail(ErrorCode.ParameterMissing);
                }

                account = req.InputAccount;
            }

            var result = await _memberService.DeleteMember(account);

            return Result.Ok(result.Value);
        }
    }
}
