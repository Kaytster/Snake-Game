using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //Used to create the list that holds the items in each menu
    class MenuItems
    {
        public string Name { get; private set; }
        public Action Selected { get; private set; }
        public string ActionKey { get; private set; }

        public MenuItems(string name, Action selected, string actionKey)
        {
            Name = name;
            Selected = selected;
            ActionKey = actionKey;
        }
    }
}
