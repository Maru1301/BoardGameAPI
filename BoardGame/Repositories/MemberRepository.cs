using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;

namespace BoardGame.Repositories
{
    public class MemberRepository : IRepository, IMemberRepository
    {
        private readonly AppDbContext _db;

        public MemberRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CheckAccountExist(string account)
        {
            var entity = _db.Members.Where(m => m.Account == account).SingleOrDefault();

            return entity != null;
        }

        public bool CheckNameExist(string nickName)
        {
            var entity = _db.Members.Where(m => m.Name == nickName).SingleOrDefault();

            return entity != null;
        }

        public bool CheckEmailExist(string email)
        {
            var entity = _db.Members.Where(m => m.Email == email).SingleOrDefault();

            return entity != null;
        }

        public void Register(MemberRegisterDTO dto)
        {
        }

        public MemberDTO SearchByAccount(string account)
        {
            return new MemberDTO();
        }
    }
}