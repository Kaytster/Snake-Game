using Programming_Assessment.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Rewards
{
    //List of Snake Skins
    class SnakeSkins : MenuBase
    {
       public SnakeSkins()
        {
            Items = new List<MenuItems>();
            var unlocker = new UnlockRewards();

            foreach (var kvp in SkinData.Cost)
            {
                string sName = kvp.Key;
                int cost = kvp.Value;

                //Show the name and cost of the skin
                string menuTitle = $"{sName.ToUpper()} SNAKE";
                string costOfSkin = $"COST: {cost}";



                //Items.Add(new MenuItems(
                //    menuTitle,
                //    () => unlocker.UnlockSkinSnake(sName, cost),
                //    actionKey: "UNLOCKREWARD"
                //));

                Items.Add(new MenuItems(
                    menuTitle,
                    null,
                    actionKey: "S_" + sName.ToUpper()
                ));
            }
            Items.Add(new MenuItems("BACK", null, "REWARDS"));

            //Items = new List<MenuItems>
            //{
            //    new MenuItems("LIME SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("WHITE SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("MAROON SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("GREY SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("MAGENTA SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("RED SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("BLUE SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("NAVY SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("TEAL SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("YELLOW SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("CYAN SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("GREEN SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("OLIVE SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("SILVER SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("PURPLE SNAKE", () => Console.WriteLine("")),
            //    new MenuItems("BACK", () => new MainMenu().ShowMenu())
            //};
        }

        public static class SkinData
        {
            public static readonly Dictionary<string, int> Cost = new Dictionary<string, int>
            {
                {"LIME", 0},
                {"WHITE", 10},
                {"MAROON", 25},
                {"GREY", 40},
                {"MAGENTA", 70},
                {"RED", 70},
                {"BLUE", 150},
                {"NAVY", 200},
                {"TEAL", 200},
                {"YELLOW", 300},
                {"CYAN", 300},
                {"GREEN", 350},
                {"OLIVE", 350},
                {"SILVER", 400},
                {"PURPLE", 450},
            };
        }
    }
}