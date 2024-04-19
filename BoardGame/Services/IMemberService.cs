using BoardGame.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;

namespace BoardGame.Services
{
    public interface IMemberService
    {
        public string Register(MemberRegisterDTO dto, string confirmationUrlTemplate);

        public string ActivateRegistration(string memberId, string confirmCode);
    }
}
