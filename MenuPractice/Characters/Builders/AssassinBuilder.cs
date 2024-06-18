using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    internal class AssassinBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

        public void BuildName()
        {
            _character.Name = "刺客";
        }

        public void BuildRule()
        {
            _character.Rule = "刺客之道";
        }

        public void BuildCards()
        {
            List<int> cards = [2, 2, 5];
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "匕首數 - 皇冠數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "匕首數 > 5";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "匕首數 - 皇冠數 - 2";
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
                    return Result.Draw;
                case (Card.Crown, Card.Shield):
                    return Result.BasicWin;
                case (Card.Crown, Card.Dagger):
                    return Result.BasicLose;
                case (Card.Shield, Card.Crown):
                    return Result.BasicLose;
                case (Card.Shield, Card.Shield):
                    return Result.Draw;
                case (Card.Shield, Card.Dagger):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Crown):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Shield):
                    return Result.BasicLose;
                case (Card.Dagger, Card.Dagger):
                    if (player1Info.Cards[2] > player2Info.Cards[2] && player1Info.Cards[2] > 2)
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[2] < player2Info.Cards[2] && player2Info.Cards[2] > 2)
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
