using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //Menu for clan functions:
    // -    Clan Selection
    // -    VCurrent Selected Clan
    //Inherits from MenuBase
    class ClanMenu : MenuBase
    {
        public ClanMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Title = "CLAN SELECTION";
            Console.ResetColor();
            Items = new List<MenuItems>
            {
                new MenuItems ("The VIPERS", null, "CLAN_V"),
                new MenuItems ("The COBRAS", null, "CLAN_C"),
                new MenuItems ("The PYTHONS", null, "CLAN_P"),
                new MenuItems ("The TITANS", null, "CLAN_T"),
                new MenuItems ("The MAMBAS", null, "CLAN_M"),
                new MenuItems("BACK", null, "ACCOUNT")
            };
        }

        protected override void Draw(int selectedIndex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Title);
            Console.ResetColor();

            string currentClan = SnakeGame.CurrentUser.Clan;

            if (!string.IsNullOrEmpty(currentClan) && currentClan != "None")
            {
                Console.WriteLine($"   CURRENT CLAN: {currentClan.ToUpper()}");
            }
            else
            {
                Console.WriteLine("\n--- YOU ARE NOT IN A CLAN ---");
                Console.WriteLine("Select one below to join:\n");
                
            }
            base.Draw(selectedIndex);
        }
    }
}
