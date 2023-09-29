using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    internal class KnightBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public KnightBuilder()
        {
            _character = new Character();
        }

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
            List<int> cards = new(){ 3, 5, 1 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "皇冠數 - 匕首數 < 1";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "匕首數  = 0";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "皇冠數";
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
                case (0, 1):
                    return Result.BasicWin;
                case (0, 2):
                    if (player1Info.Cards[2] > player2Info.Cards[2])
                    {
                        return Result.Draw;
                    }
                    return Result.BasicLose;
                case (1, 0):
                    return Result.BasicLose;
                case (1, 1):
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
                case (1, 2):
                    return Result.BasicWin;
                case (2, 0):
                    if(player1Info.Cards[2] < player2Info.Cards[2])
                    {
                        return Result.Draw;

                    }
                    return Result.BasicWin;
                case (2, 1):
                    return Result.BasicLose;
                case (2, 2):
                    if(player1Info.Cards[2] > player2Info.Cards[2])
                    {
                        return Result.CharacterRuleWin;
                    }else if(player1Info.Cards[2] < player2Info.Cards[2])
                    {
                        return Result.CharacterRuleLose;
                    }
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
