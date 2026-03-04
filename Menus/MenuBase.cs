using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //The base class for all the menus.
    //Holds the functionality for cycling through the menu
    class MenuBase
    {
        protected List<MenuItems> Items = new List<MenuItems>();
        protected string Title { get; set; }
        protected Action Action { get; set; }
        protected int MenuStartRow = 0;
        
        public string ShowMenu(bool clearScreen = true)
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            int index = 0;
            Draw(index);

            ConsoleKeyInfo key;

            //Movement through the menu using arrow keys
            while (true)
            {
                key = Console.ReadKey(true);
                bool selectionChanged = false;
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + Items.Count) % Items.Count;
                        selectionChanged = true;
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % Items.Count;
                        selectionChanged = true;
                        break;
                    case ConsoleKey.Enter:
                        //Items[index].Selected.Invoke();
                        //string selectedAction = Items[index].actionKey;
                        //return selectedAction;
                        return Items[index].ActionKey;
                    case ConsoleKey.X:
                        return "MENU";
                }
                if (selectionChanged)
                {
                    Draw(index);
                }
                //Draw(index);
            }
        }

        protected virtual void Draw(int selectedIndex)
        {
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Title);
            Console.ResetColor();

            if (MenuStartRow == 0)
            {
                MenuStartRow = Console.CursorTop;
            }
            Console.SetCursorPosition(0, MenuStartRow);

            for (int i = 0; i < Items.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                string prefix = "  ";

                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    prefix = "> ";
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                string fullLine = $"{prefix}{Items[i].Name}";
                string outputText = fullLine.PadRight(Console.WindowWidth - 1);

                Console.Write(outputText);
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}
