using Programming_Assessment.Accounts;
using Programming_Assessment.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Programming_Assessment.Rewards
{
    //Unlocking rewards
    class UnlockRewards : MenuBase
    {
        public string UnlockSkinSnake(string sName, int reqScore)
        {
            int userScore = SnakeGame.CurrentUser.GetHighScore();
            bool isUnlocked = SnakeGame.CurrentUser.IsSkinUnlocked(sName);
            ConsoleColor sColour = SkinColours.GetColour(sName);
            int pointsNeeded = (reqScore - userScore);
            //Get the score, if skin is unlocked already, and points needed.

            //Preview of the skin
            Console.Clear();
            Console.WriteLine($"Your Score: {userScore}");
            Console.WriteLine("");
            Console.WriteLine($"{sName.ToUpper()}");
            Console.ForegroundColor = sColour;
            Console.WriteLine("■■■■■■■■■■");
            Console.ResetColor();
            Console.WriteLine($"COST: {reqScore}");
            this.MenuStartRow = Console.CursorTop + 1;

            if (isUnlocked) //If it is unlocked, Use equipped function
            {
                bool isEquipped = sName.Equals(SnakeGame.CurrentUser.GetEquippedSnakeSkin(), StringComparison.OrdinalIgnoreCase);

                Items = new List<MenuItems>
                {
                    new MenuItems(isEquipped ? "EQUIPPED" : "EQUIP", null, isEquipped ? "SNAKE" : $"EQUIP_COMPLETE_{sName}"),
                    new MenuItems("BACK", null, "SNAKE")
                };
            }
            else if (userScore >= reqScore) //If not unlocked but have enough points, Use Unlock function
            {
                Items = new List<MenuItems>
                {
                    new MenuItems("O UNLOCK O", null, "PERFORM_UNLOCK"),
                    new MenuItems("BACK", null, "SNAKE")
                };
            }
            else //If not enough points show unavailable
            {
                Items = new List<MenuItems>
                {
                    new MenuItems($"X UNLOCK X ({pointsNeeded} more Points Needed!)", null, "UNAVAILABLE"),
                    new MenuItems("BACK", null, "SNAKE")
                };
            }

            string nextAction = this.ShowMenu(false);

            if (nextAction == "PERFORM_UNLOCK") //Unlock the skin, save to account, and show success message
            {
                SnakeGame.CurrentUser.UnlockSkin(sName);
                SaveAccount.saveUpdatedAccount();

                List<UserAccount> allAccounts = SnakeGame.ReadAllAccounts();
                UserAccount freshAccount = allAccounts.FirstOrDefault(acc => acc.GetID() == SnakeGame.CurrentUser.GetID());

                if (freshAccount != null)
                {
                    SnakeGame.CurrentUser = freshAccount;

                    if (SnakeGame.CurrentUser.IsSkinUnlocked(sName))
                    {
                        unlockMessagesSnake("Skin Unlocked Successfully!", 2000);
                        return UnlockSkinSnake(sName, reqScore);
                    }
                }
                unlockMessagesSnake("Error: Could not unlock skin.", 2000);
                return "SNAKE";
            }
            else if (nextAction.StartsWith("EQUIP_COMPLETE_")) //Equip the skin, save to account, and show success message
            {
                string skinToEquip = nextAction.Substring(15);
                SnakeGame.CurrentUser.EquipSnakeSkin(skinToEquip);
                SaveAccount.saveUpdatedAccount();
                unlockMessagesSnake($"Successfully equipped {skinToEquip.ToUpper()}!", 2000);
                return "SNAKE";
                //return UnlockSkinSnake(sName, reqScore);
            }
            else if (nextAction == "EQUIP")
            {
                return "SNAKE";
            }
            else
            {
                return nextAction;
            }

           
        }

        public string UnlockSkinFruit(string fName, int reqScore)
        {
            int userScore = SnakeGame.CurrentUser.GetHighScore();
            bool isUnlocked = SnakeGame.CurrentUser.IsSkinUnlocked(fName);
            ConsoleColor sColour = SkinColours.GetColour(fName);
            int pointsNeeded = (reqScore - userScore);
            //Get the score, if skin is unlocked already, and points needed.

            Console.Clear();
            Console.WriteLine($"Your Score: {userScore}");
            Console.WriteLine("");
            Console.WriteLine($"{fName.ToUpper()}");
            Console.ForegroundColor = sColour;
            char displayChar = SkinColours.GetChar(fName);
            Console.WriteLine(displayChar);
            Console.ResetColor();
            Console.WriteLine($"COST: {reqScore}");
            this.MenuStartRow = Console.CursorTop + 1;

            if (isUnlocked)
            {
                bool isEquipped = fName.Equals(SnakeGame.CurrentUser.GetEquippedFruitSkin(), StringComparison.OrdinalIgnoreCase);

                Items = new List<MenuItems>
                {
                    new MenuItems(isEquipped ? "EQUIPPED" : "EQUIP", null, isEquipped ? "FRUIT" : $"EQUIP_COMPLETE_{fName}"),
                    new MenuItems("BACK", null, "FRUIT")
                };
            }
            else if (userScore >= reqScore)
            {
                Items = new List<MenuItems>
                {
                    new MenuItems("O UNLOCK O", null, "PERFORM_UNLOCK"),
                    new MenuItems("BACK", null, "FRUIT")
                };
            }
            else
            {
                Items = new List<MenuItems>
                {
                    new MenuItems($"X UNLOCK X ({pointsNeeded} more Points Needed!)", null, "UNAVAILABLE"),
                    new MenuItems("BACK", null, "FRUIT")
                };
            }

            string nextAction = this.ShowMenu(false);

            if (nextAction == "PERFORM_UNLOCK")
            {
                SnakeGame.CurrentUser.UnlockSkin(fName);
                SaveAccount.saveUpdatedAccount();

                List<UserAccount> allAccounts = SnakeGame.ReadAllAccounts();
                UserAccount freshAccount = allAccounts.FirstOrDefault(acc => acc.GetID() == SnakeGame.CurrentUser.GetID());

                if (freshAccount != null)
                {
                    SnakeGame.CurrentUser = freshAccount;

                    if (SnakeGame.CurrentUser.IsSkinUnlocked(fName))
                    {
                        unlockMessagesFruit("Skin Unlocked Successfully!", 2000);
                        return UnlockSkinFruit(fName, reqScore);
                    }
                }
                unlockMessagesFruit("Error: Could not unlock skin.", 2000);
                return "FRUIT";
            }
            else if (nextAction.StartsWith("EQUIP_COMPLETE_"))
            {
                string skinToEquip = nextAction.Substring(15);
                SnakeGame.CurrentUser.EquipFruitSkin(skinToEquip);
                SaveAccount.saveUpdatedAccount();
                unlockMessagesFruit($"Successfully equipped {skinToEquip.ToUpper()}!", 2000);
                //return "SNAKE";
                //return UnlockSkinFruit(fName, reqScore);
                return "FRUIT";
            }
            else if (nextAction == "EQUIP")
            {
                return "FRUIT";
            }
            else
            {
                return nextAction;
            }


        }

        public static class SkinColours
        {
            public static readonly Dictionary<string, ConsoleColor> SkinColourMap = new Dictionary<string, ConsoleColor>(StringComparer.OrdinalIgnoreCase)
            {
                {"Lime", ConsoleColor.Green },
                {"Kiwi", ConsoleColor.Green },
                {"White", ConsoleColor.White },
                {"Maroon", ConsoleColor.DarkRed },
                {"Grey", ConsoleColor.DarkGray },
                {"Magenta", ConsoleColor.Magenta },
                {"Peach", ConsoleColor.Magenta },
                {"Red", ConsoleColor.Red },
                {"Apple", ConsoleColor.Red },
                {"Blue", ConsoleColor.Blue },
                {"Blueberry", ConsoleColor.Blue },
                {"Navy", ConsoleColor.DarkBlue },
                {"Teal", ConsoleColor.DarkCyan },
                {"Yellow", ConsoleColor.Yellow },
                {"Lemon", ConsoleColor.Yellow },
                {"Cyan", ConsoleColor.Cyan },
                {"Green", ConsoleColor.DarkGreen },
                {"Watermelon", ConsoleColor.DarkGreen },
                {"Olive", ConsoleColor.DarkYellow },
                {"Banana", ConsoleColor.DarkYellow },
                {"Silver", ConsoleColor.Gray },
                {"Purple", ConsoleColor.DarkMagenta },
                {"Grape", ConsoleColor.DarkMagenta },
            };

            public static readonly Dictionary<string, char> FruitCharMap = new Dictionary<string, char>(StringComparer.OrdinalIgnoreCase)
            {
                {"Peach", 'P' },
                {"Kiwi", 'K' },
                {"Apple", 'A' },
                {"Blueberry", 'B' },
                {"Lemon", 'L' },
                {"Watermelon", 'W' },
                {"Banana", 'B' },
                {"Grape", 'G' },
            };

            public static ConsoleColor GetColour(string sName)
            {
                if (SkinColourMap.TryGetValue(sName, out ConsoleColor colour))
                {
                    return colour;
                }
                return ConsoleColor.Green;
            }

            public static char GetChar(string fName)
            {
                if (FruitCharMap.TryGetValue(fName, out char fruitChar))
                {
                    return fruitChar;
                }
                return '0';
            }
        }

        public string unlockMessagesSnake(string message, int duration = 2000)
        {
            Console.Clear();
            Console.WriteLine($"{message}");
            Thread.Sleep(duration);
            //Action alertMessage = () =>
            //{

            //    //new SnakeSkins().ShowMenu();
            //};
            return "SNAKE";
            //return alertMessage;
        }
        public string unlockMessagesFruit(string message, int duration = 2000)
        {
            Console.Clear();
            Console.WriteLine($"{message}");
            Thread.Sleep(duration);
            //Action alertMessage = () =>
            //{

            //    //new SnakeSkins().ShowMenu();
            //};
            return "FRUIT";
            //return alertMessage;
        }
    }
}
