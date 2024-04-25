using BoardGame.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        public IMongoClient GetMongoClient();
        public IEnumerable<MemberDTO> GetAll();
        public bool CheckAccountExist(string account);
        public bool CheckNameExist(string nickName);
        public bool CheckEmailExist(string email);
        public void Register(RegisterDTO dto);
        public void ActivateRegistration(string memberId);
        public Task<MemberDTO?> SearchByAccount(string account);
        public MemberDTO? SearchById(string id);
    }
}
