namespace Menu_Practice.Characters.Builders
{
    internal class LobbyistBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

        public void BuildName()
        {
            _character.Name = "說客";
        }

        public void BuildRule()
        {
            _character.Rule = "外交手腕";
        }

        public void BuildCards()
        {
            List<int> cards = [5, 2, 2];
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "皇冠數 - 盾牌數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "皇冠數 > 5";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "盾牌數 + 1";
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
                    if (player1Info.Cards[0] > 2 && player1Info.Cards[0] > player2Info.Cards[0])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player2Info.Cards[0] > 2 && player2Info.Cards[0] > player1Info.Cards[0])
                    {
                        return Result.CharacterRuleLose;
                    }
                    else
                    {
                        return Result.Draw;
                    }
                case (Card.Crown, Card.Shield):
                    return Result.BasicWin;
                case (Card.Crown, Card.Dagger):
                    return Result.BasicLose;
                case (Card.Shield, Card.Crown):
                    return Result.BasicLose;
                case (Card.Shield, Card.Shield):
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
                case (Card.Shield, Card.Dagger):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Crown):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Shield):
                    return Result.BasicLose;
                case (Card.Dagger, Card.Dagger):
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
