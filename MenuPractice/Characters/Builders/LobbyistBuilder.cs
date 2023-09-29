using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    internal class LobbyistBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public LobbyistBuilder()
        {
            _character = new Character();
        }

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
            List<int> cards = new() { 5, 2, 2 };
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

        public Result RuleLogic(PlayerInfoContainer player1Info, PlayerInfoContainer player2Info)
        {
            switch ((player1Info.ChosenCard, player2Info.ChosenCard))
            {
                case (0, 0):
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
                case (0, 1):
                    return Result.BasicWin;
                case (0, 2):
                    return Result.BasicLose;
                case (1, 0):
                    return Result.BasicLose;
                case (1, 1):
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
                case (1, 2):
                    return Result.BasicWin;
                case (2, 0):
                    return Result.BasicWin;
                case (2, 1):
                    return Result.BasicLose;
                case (2, 2):
                    return Result.Draw;
            }

            return Result.Draw;
        }


        public Character GetCharacter()
        {
            return _character;
        }
    }
}
