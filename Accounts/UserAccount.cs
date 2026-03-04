using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    //User account for logged in users. Inherits from account class
    class UserAccount : Accounts
    {
        //Stores high score, snake skin, fruit skin, and clan
        protected int HighScore;
        protected string equippedSnakeSkin;
        protected string equippedFruitSkin;
        public string Clan { get; set; }

        public UserAccount(int userID, string username, string password, int highscore, string snakeSkin = "LIME", string fruitSkin = "APPLE", string clan = "None")
        : base(userID, username, password) // Calls the base constructor
        {
            HighScore = highscore; // Initializes the HighScore field in UserAccount
            UnlockSkin("LIME"); //Automatically unlocked the lime skin as it is the default skin
            UnlockSkin("APPLE"); //Automatically unlocked the apple skin as it is the default fruit
            this.equippedSnakeSkin = snakeSkin;
            this.equippedFruitSkin = fruitSkin;
            this.Clan = clan;
        }

        public string GetEquippedSnakeSkin()
        {
            return equippedSnakeSkin;
        }
        public string GetEquippedFruitSkin()
        {
            return equippedFruitSkin;
        }

        public void EquipSnakeSkin(string skinName)
        {
            if (IsSkinUnlocked(skinName))
            {
                this.equippedSnakeSkin = skinName;
            }
            else
            {
                Console.WriteLine("Cannot Equip Skin");
            }
        }
        public void EquipFruitSkin(string skinName)
        {
            if (IsSkinUnlocked(skinName))
            {
                this.equippedFruitSkin = skinName;
            }
            else
            {
                Console.WriteLine("Cannot Equip Skin");
            }
        }

        public int GetHighScore() { return HighScore; }

        public void SetHighScore(int newScore)
        {
            HighScore = newScore;
        }

        private List<string> unlockedSkins = new List<string>();
        public bool IsSkinUnlocked(string sName)
        {
            return unlockedSkins.Any(s => s.Equals(sName, StringComparison.OrdinalIgnoreCase));
        }
        public void UnlockSkin(string sName)
        {
            if (!IsSkinUnlocked(sName))
            {
                unlockedSkins.Add(sName);
            }
        }

        public List<string> GetUnlockedSkins()
        {
            return unlockedSkins;
        }

        public void InitializeSkins(List<string> skins)
        {
            unlockedSkins = skins;
        }

        public string GetTier() //Get the user tier depending on their score. New users are automatically set to Bronze
        {
            if (HighScore >= 50) return "SILVER";
            if (HighScore >= 100) return "GOLD";
            if (HighScore >= 250) return "DIAMOND";
            if (HighScore >= 500) return "EMERALD";
            return "BRONZE";
        }
        public ConsoleColor GetTierColor()
        {
            return GetTier() switch
            {
                "SILVER" => ConsoleColor.Gray,
                "GOLD" => ConsoleColor.DarkYellow,
                "DIAMOND" => ConsoleColor.Cyan,
                "EMERALD" => ConsoleColor.Green,
                _ => ConsoleColor.DarkRed //BRONZE
            };
        }
    }
}
