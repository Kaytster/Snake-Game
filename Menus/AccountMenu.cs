using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //Menu for account functions:
    // -    Edit Account
    // -    View High Score
    // -    View Tier
    // -    View Clan
    // -    Create Account
    //Inherits from MenuBase
    class AccountMenu : MenuBase
    {
        public AccountMenu()
        {
            Title = @"
            
                 __  __            _                             _   
                |  \/  |_   _     / \   ___ ___ ___  _   _ _ __ | |_ 
                | |\/| | | | |   / _ \ / __/ __/ _ \| | | | '_ \| __|
                | |  | | |_| |  / ___ \ (_| (_| (_) | |_| | | | | |_ 
                |_|  |_|\__, | /_/   \_\___\___\___/ \__,_|_| |_|\__|
                        |___/                                        
                                     
            ";

            if (SnakeGame.isLoggedIn == true)
            {
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine($"Welcome {SnakeGame.CurrentUser.GetUsername()} !");
                //Console.ResetColor();
                


                Items = new List<MenuItems>
                {
                    new MenuItems("Edit Account", () => new Accounts.EditAccount().ShowMenu(), "EDIT"),
                    new MenuItems("View Clan", () => new Menus.ClanMenu().ShowMenu(), "CLAN"),
                    new MenuItems("BACK", () => new MainMenu().ShowMenu(), "MENU")
                };
            }
            else if (SnakeGame.isLoggedIn == false)
            {
                Items = new List<MenuItems>
                {
                    new MenuItems("CREATE ACCOUNT", () => Accounts.CreateAccount.createAccount(), "CREATE"),
                    new MenuItems("BACK", () => new MainMenu().ShowMenu(), "MENU"),
                };
            }

        }
        protected override void Draw(int selectedIndex)
        {
            base.Draw(selectedIndex); // Draws the Title and the Menu Items

            if (SnakeGame.isLoggedIn && SnakeGame.CurrentUser != null)
            {
                Console.WriteLine(); // Add a gap
                ShowDetails();
            }
        }

        public void ShowDetails()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" ACCOUNT INFORMATION");
            Console.ResetColor();
            Console.WriteLine($"Username: {SnakeGame.CurrentUser.GetUsername()}");
            Console.ForegroundColor = SnakeGame.CurrentUser.GetTierColor();
            Console.WriteLine($"Tier: {SnakeGame.CurrentUser.GetTier()}");
            Console.ResetColor();
            Console.WriteLine($"High Score: {SnakeGame.CurrentUser.GetHighScore()}");
            Console.WriteLine($"Clan: {SnakeGame.CurrentUser.Clan}");
        }
    }
}
