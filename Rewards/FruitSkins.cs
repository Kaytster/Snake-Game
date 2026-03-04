using Programming_Assessment.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programming_Assessment.Rewards.SnakeSkins;

namespace Programming_Assessment.Rewards
{
    //List of Fruit Skins
    class FruitSkins : MenuBase
    {
        public FruitSkins()
        {
            Items = new List<MenuItems>();
            var unlocker = new UnlockRewards();

            foreach (var kvp in FruitSkins.SkinData.Cost)
            {
                string fName = kvp.Key;
                int cost = kvp.Value;

                //Show the name and cost of the skin
                string menuTitle = $"{fName.ToUpper()}";
                string costOfSkin = $"COST: {cost}";

                Items.Add(new MenuItems(
                    menuTitle,
                    null,
                    actionKey: "F_" + fName.ToUpper()
                ));
            }
            Items.Add(new MenuItems("BACK", null, "REWARDS"));
        }

        public static class SkinData
        {
            public static readonly Dictionary<string, int> Cost = new Dictionary<string, int>
            {
                {"APPLE", 0}, //APPLE
                {"KIWI", 10}, //KIWI
                {"PEACH", 70}, //PEACH
                {"BLUEBERRY", 150}, //BLUEBERRY
                {"LEMON", 300}, //LEMON
                {"WATERMELON", 350}, //WATERMELON
                {"BANANA", 350}, //BANANA
                {"GRAPE", 450}, //GRAPE
            };
        }
    }
}
