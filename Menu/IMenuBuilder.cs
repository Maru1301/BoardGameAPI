using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Menu
{
    internal interface IMenuBuilder
    {
        MenuList BuildMainMenuList(string title);

        MenuList BuildCharacterMenuList(string title);

        MenuList BuildOpponentMenuList(string title);

        void ConnectMenuOptionWithMenuList(MenuOption menuOption, MenuList menuList);

        MenuList GetRootMenuList();
    }
}
