using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Menus
{
    //Menu for rewards: 
    // -    View All Rewards
    // -    Redeem Available Rewards
    //Inherits from MenuBase
    class RewardsMenu : MenuBase
    {
        public RewardsMenu()
        {
            Title = @"
            
                 ____                            _     
                |  _ \ _____      ____ _ _ __ __| |___ 
                | |_) / _ \ \ /\ / / _` | '__/ _` / __|
                |  _ <  __/\ V  V / (_| | | | (_| \__ \
                |_| \_\___| \_/\_/ \__,_|_|  \__,_|___/
                                                 
            ";

            Items = new List<MenuItems>
            {
                new MenuItems("SNAKE", () => new Rewards.SnakeSkins().ShowMenu(), "SNAKE"),
                new MenuItems("FRUIT", () => new Rewards.FruitSkins().ShowMenu(), "FRUIT"),
                new MenuItems("BACK", () => new MainMenu().ShowMenu(), "MENU")
            };
        }
    }
}
