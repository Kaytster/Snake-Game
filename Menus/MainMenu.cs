using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //The game's main menu:
    // -    Start The Game
    // -    View Leaderboard
    // -    View Account
    // -    View Leaderboard
    // -    Exit Program
    //Inherits from MenuBase
    class MainMenu : MenuBase
    {
        public MainMenu()
        {
            Title = @"
            
                 __  __       _         __  __                  
                |  \/  |     (_)       |  \/  |                 
                | \  / | __ _ _ _ __   | \  / | ___ _ __  _   _ 
                | |\/| |/ _` | | '_ \  | |\/| |/ _ \ '_ \| | | |
                | |  | | (_| | | | | | | |  | |  __/ | | | |_| |
                |_|  |_|\__,_|_|_| |_| |_|  |_|\___|_| |_|\__,_|
                                                 
            ";

            Items = new List<MenuItems>
            {
                new MenuItems("START GAME", () => { }, "START"),
                new MenuItems("LEADERBOARD", () => new Leaderboard().ShowMenu(), "LEADERBOARD"),
                new MenuItems("ACCOUNT", () => new AccountMenu().ShowMenu(), "ACCOUNT"),
                new MenuItems("REWARDS", () => new RewardsMenu().ShowMenu(), "REWARDS"),
                new MenuItems("EXIT", () => Environment.Exit(0), "EXIT")
            };

        }
    }
}
