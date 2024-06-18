namespace Menu_Practice.Characters.Builders
{
    internal class KnightBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

        public void BuildName()
        {
            _character.Name = "騎士";
        }

        public void BuildRule()
        {
            _character.Rule = "騎士精神";
        }

        public void BuildCards()
        {
            List<int> cards = [3, 5, 1];
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "皇冠數 - 匕首數 < 1";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "匕首數  = Card.Crown";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "皇冠數";
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
                    if (player1Info.Cards[2] < player2Info.Cards[2])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[2] > player2Info.Cards[2])
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
                    if (player1Info.Cards[2] > player2Info.Cards[2])
                    {
                        return Result.Draw;
                    }
                    return Result.BasicLose;
                case (Card.Shield, Card.Crown):
                    return Result.BasicLose;
                case (Card.Shield, Card.Shield):
                    if (player1Info.Cards[2] < player2Info.Cards[2])
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[2] == player2Info.Cards[2])
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
                    if(player1Info.Cards[2] < player2Info.Cards[2])
                    {
                        return Result.Draw;

                    }
                    return Result.BasicWin;
                case (Card.Dagger, Card.Shield):
                    return Result.BasicLose;
                case (Card.Dagger, Card.Dagger):
                    if(player1Info.Cards[2] > player2Info.Cards[2])
                    {
                        return Result.CharacterRuleWin;
                    }else if(player1Info.Cards[2] < player2Info.Cards[2])
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
