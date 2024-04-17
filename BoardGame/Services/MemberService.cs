namespace BoardGame.Services
{
    public class MemberService
    {
        public (bool Success, string Message) Register(MemberRegisterDTO dto, string urlTemplate)
        {
            if (_memberRepository.IsExist(dto.MemberAccount!))
            {
                return (false, "帳號已存在");
            }
            // 驗證暱稱是否重複
            if (_memberRepository.NickNameExist(dto.MemberNickName!))
            {
                return (false, "暱稱已存在");
            }

            if (_memberRepository.EmailExist(dto.MemberEmail!))
            {
                return (false, "信箱已存在");
            }

            dto.ConfirmCode = Guid.NewGuid().ToString("N");

            _memberRepository.MemberRegister(dto);

            MemberDTO entity = _memberRepository.GetByAccount(dto.MemberAccount);
            // 發email
            string url = string.Format(urlTemplate, entity.Id, dto.ConfirmCode);

            new EmailHelper().SendConfirmRegisterEmail(url, dto.MemberNickName!, dto.MemberEmail!);

            // 創建播放佇列
            _queueRepository.CreateQueue(entity.Id);

            return (true, "註冊成功，已發送驗證信");
        }
    }
}
