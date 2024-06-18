using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    internal class DeceiverBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

        public void BuildName()
        {
            _character.Name = "詐欺師*";
        }

        public void BuildRule()
        {
            _character.Rule = "誘敵戰術";
        }

        public void BuildCards()
        {
            List<int> cards = [4, 1, 4];
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "匕首數 - 盾牌數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "盾牌數 = 0";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "匕首數 - 2";
        }

        public void BuildGameLogic()
        {
            _character.UseRuleLogic = RuleLogic;
        }

        private static Result RuleLogic(PlayerInfoContainer player1Info, PlayerInfoContainer player2Info)
        {
            switch ((player1Info.ChosenCard, player2Info.ChosenCard))
            {
                case (Card.Crown, Card.Crown):
                    if (player1Info.Cards[1] > player2Info.Cards[1])
                    {
                        return Result.CharacterRuleWin;
                    }else if(player1Info.Cards[1] < player2Info.Cards[1])
                    {
                        return Result.CharacterRuleLose;
                    }
                    return Result.Draw;
                case (Card.Crown, Card.Shield):
                    return Result.BasicWin;
                case (Card.Crown, Card.Dagger):
                    return Result.BasicLose;
                case (Card.Shield, Card.Crown):
                    return Result.BasicLose;
                case (Card.Shield, Card.Shield):
                    if (player1Info.Cards[1] < player2Info.Cards[1])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[1] > player2Info.Cards[1])
                    {
                        return Result.CharacterRuleLose;
                    }
                    return Result.Draw;
                case (Card.Shield, Card.Dagger):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Crown):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Shield):
                    return Result.BasicLose;
                case (Card.Dagger, Card.Dagger):
                    if (player1Info.Cards[1] < player2Info.Cards[1])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[1] > player2Info.Cards[1])
                    {
                        return Result.CharacterRuleLose;
                    }
                    return Result.Draw;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
