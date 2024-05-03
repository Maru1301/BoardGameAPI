using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class MemberRepository(AppDbContext dbContext) : IRepository, IMemberRepository
    {
        private readonly AppDbContext _db = dbContext;

        public IEnumerable<MemberDTO> GetAll()
        {
            var members = _db.Members.ToList();

            return members.Select(m => m.ToDTO<MemberDTO>());
        }

        public void Register(RegisterDTO dto)
        {
            _db.Members.Add(dto.ToEntity<Member>());
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

        public async Task<MemberDTO?> SearchById(string id)
        {
            var member = await _db.Members.FirstOrDefaultAsync(member => member.Id.ToString() == id);

            return member?.ToDTO<MemberDTO>();
        }

        public async Task<MemberDTO?> SearchByName(string name)
        {
            var member = await _db.Members.FirstOrDefaultAsync(x => x.Name == name);

            return member?.ToDTO<MemberDTO>();
        }

        public async Task<MemberDTO?> SearchByEmail(string email)
        {
            var member = await _db.Members.FirstOrDefaultAsync(x => x.Email == email);

            return member?.ToDTO<MemberDTO>();
        }
    }
}