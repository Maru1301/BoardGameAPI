using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    class LordBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public LordBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.Name = "領主";
        }

        public void BuildRule()
        {
            _character.Rule = "[軍閥割據]\r\n剝奪極權統治者的權利, 一切訴諸武力\r\n1. 以下表格中, [↑皇] 為手牌中皇冠較多的一方, 反之\r\n2. 手牌中皇冠少的獲勝，但手牌中皇冠少的玩家若沒有皇冠則平手\r\n\t[皇冠]\t[盾牌]\t[匕首]\r\n[皇冠]\t[↑皇]\t[皇冠]\t[匕首]\r\n[盾牌]\t[2]\t[↑盾]\t[盾牌]\r\n[匕首]\t[匕首]\t[盾牌]\t[↓皇]\r\n";
        }

        public void BuildCards()
        {
            List<int> cards = new() { 0, 6, 3 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "盾牌數 - 皇冠數 < 5";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "皇冠數 = 0";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "盾牌數";
        }

        public void BuildGameLogic()
        {
            _character.UseRuleLogic = RuleLogic;
        }

        public Result RuleLogic(PlayerInfoContainer player1Info, PlayerInfoContainer player2Info)
        {
            switch ((player1Info.ChosenCard, player2Info.ChosenCard))
            {
                case (0, 0):
                    if (player1Info.Cards[0] > player2Info.Cards[0])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[0] == player2Info.Cards[0])
                    {
                        return Result.Draw;
                    }
                    else
                    {
                        return Result.CharacterRuleLose;
                    }
                case (0, 1):
                    return Result.BasicWin;
                case (0, 2):
                    return Result.BasicLose;
                case (1, 0):
                    if (player1Info.Cards[0] == 0 || player2Info.Cards[0] == 0)
                    {
                        return Result.Draw;
                    }
                    else
                    {
                        return Result.BasicLose;
                    }
                case (1, 1):
                    if (player1Info.Cards[1] > player2Info.Cards[1])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[1] == player2Info.Cards[1])
                    {
                        return Result.Draw;
                    }
                    else
                    {
                        return Result.CharacterRuleLose;
                    }
                case (1, 2):
                    return Result.BasicWin;
                case (2, 0):
                    return Result.BasicWin;
                case (2, 1):
                    return Result.BasicLose;
                case (2, 2):
                    if (player1Info.Cards[0] < player2Info.Cards[0])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[0] == player2Info.Cards[0])
                    {
                        return Result.Draw;
                    }
                    else
                    {
                        return Result.CharacterRuleLose;
                    }
            }

            return Result.Draw;
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
