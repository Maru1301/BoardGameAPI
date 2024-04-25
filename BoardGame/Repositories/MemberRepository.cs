using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class MemberRepository : IRepository, IMemberRepository
    {
        private readonly AppDbContext _db;

        public MemberRepository(IMongoClient client)
        {
            _db = AppDbContext.Create(client.GetDatabase("BoardGameDB"));
        }

        public DbContext GetContext()
        {
            return _db;
        }

        public IEnumerable<MemberDTO> GetAll()
        {
            var members = _db.Members.ToList();

            return members.Select(m => m.ToDTO<MemberDTO>());
        }

        public bool CheckAccountExist(string account)
        {
            var entity = _db.Members.FirstOrDefault(m => m.Account == account);

            return entity != null;
        }

        public bool CheckNameExist(string nickName)
        {
            var entity = _db.Members.FirstOrDefault(m => m.Name == nickName);

            return entity != null;
        }

        public bool CheckEmailExist(string email)
        {
            var entity = _db.Members.FirstOrDefault(m => m.Email == email);

            return entity != null;
        }

        public void Register(RegisterDTO dto)
        {
            var member = new Member
            {
                Account = dto.Account,
                EncryptedPassword = dto.EncryptedPassword,
                Name = dto.Name,
                Email = dto.Email,
                IsConfirmed = false, //default is Unconfirmed
                Salt = dto.Salt, //default is Unactive
                ConfirmCode = dto.ConfirmCode
            };
            _db.Members.Add(member);
            _db.SaveChanges();
        }

        public void ActivateRegistration(string memberId)
        {
            var member = _db.Members.First(member => member.Id.ToString() == memberId);
            member.IsConfirmed = true;

            _db.SaveChanges();
        }

        public async Task<MemberDTO?> SearchByAccount(string account)
        {
            var member = await _db.Members.FirstOrDefaultAsync(member => member.Account == account);

            return member?.ToDTO<MemberDTO>();
        }

        public MemberDTO? SearchById(string id)
        {
            var member = _db.Members.FirstOrDefault(member => member.Id.ToString() == id);

            return member?.ToDTO<MemberDTO>();
        }
    }
}