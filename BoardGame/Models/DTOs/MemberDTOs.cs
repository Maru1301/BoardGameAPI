namespace BoardGame.Models.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
    }

    public class MemberRegisterDTO
    {
        public string Account { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string ConfirmCode { get; set; } = string.Empty;
    }
}
