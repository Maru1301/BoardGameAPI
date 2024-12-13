using MenuVisualizer.Model;
using MenuVisualizer.Model.Interface;

namespace BoardGameClient
{
    public class NewMenuBuilder
    {
        private Func<object?, object?> _setPlayerCharacter;
        private Func<object?, object?> _setBotCharacter;

        public NewMenuBuilder(Func<object?, object?> setPlayerCharacter,
            Func<object?, object?> setbotCharacter)
        {
            _setPlayerCharacter = setPlayerCharacter;
            _setBotCharacter = setbotCharacter;
        }

        public Menu Construct()
        {
            List<IOption> opponentOptions = [..GameService.GetCharacterList().Select(character =>
            {
                return new SubMenuOption()
                {
                    Name = character.Name,
                    SubMenu = new Menu()
                    {
                        Name = character.Name,
                        Descriptions =
                        [
                            character.Rule,
                            character.Card.ToString(),
                            character.Disqualification,
                            character.Evolution,
                            character.AdditionalPoint
                        ],
                        Options =
                        [
                            new FunctionOption(){
                                Name = "選擇",
                                Func = async (object? o) => {
                                    _setBotCharacter(character);
                                    return null;
                                }
                            }
                        ]
                    }
                };
            }).ToList()];

            var chooseOpponentMenu = new Menu()
            {
                Name = "請選擇敵人的角色",
                Options = opponentOptions
            };

            List<IOption> options = [..GameService.GetCharacterList().Select(character =>
            {
                return new SubMenuOption()
                {
                    Name = character.Name,
                    SubMenu = new Menu()
                    {
                        Name = character.Name,
                        Descriptions =
                        [
                            character.Rule,
                            character.Card.ToString(),
                            character.Disqualification,
                            character.Evolution,
                            character.AdditionalPoint
                        ],
                        Options =
                        [
                            new FunctionOption(){
                                Name = "選擇",
                                Func = async (object? o) => {
                                    _setPlayerCharacter(character);
                                    return null;
                                },
                                AfterFuncSubMenu = chooseOpponentMenu
                            }
                        ]
                    }
                };
            }).ToList()];

            var chooseCharacterMenu = new Menu()
            {
                Name = "請選擇你要的角色",
                Options = options
            };

            var mainMenu =  new Menu(){
                Name = "GaoSai King",
                Options = [
                    new SubMenuOption(){
                        Name = "開始遊戲",
                        SubMenu = chooseCharacterMenu
                    },
                    new FunctionOption(){
                        Name = "離開",
                        Func = async (object? obj) =>
                        {
                            await Task.Yield();
                            Environment.Exit(0);
                            return null;
                        }
                    }
                ]
            };

            return mainMenu;
        }
    }
}
