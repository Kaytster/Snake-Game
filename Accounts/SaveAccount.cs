using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    class SaveAccount
    {
        //Saving accounts to the accounts file
        public static void saveAccountsToFile(List<UserAccount> accounts)
        {
            try
            {
                using (FileStream fs = new FileStream(SnakeGame.accountsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //Writing all information to the accounts file
                    foreach (var account in accounts)
                    {
                        bw.Write(account.GetID());
                        bw.Write(account.GetUsername());
                        bw.Write(account.GetPassword());
                        bw.Write(account.GetHighScore());

                        List<string> skinsToSave = account.GetUnlockedSkins();
                        bw.Write(skinsToSave.Count);
                        foreach (string skin in skinsToSave)
                        {
                            bw.Write(skin);
                        }
                        bw.Write(account.GetEquippedSnakeSkin());
                        bw.Write(account.GetEquippedFruitSkin());
                        bw.Write(account.Clan ?? "None");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Could not save");
                Console.ResetColor();
                Thread.Sleep(3000);
                new Menus.AccountMenu().ShowMenu();
            }
        }
        //Saving accounts to the file after they have been updated when a user changes their username or password
        public static int saveUpdatedAccount()
        {
            List<UserAccount> accounts = SnakeGame.ReadAllAccounts();
            int userId = SnakeGame.CurrentUser.GetID();
            int index = accounts.FindIndex(acc => acc.GetID() == userId);
            //int index = accounts.FindIndex(acc => acc.GetID() == SnakeGame.CurrentUser.GetID());
            if (index != -1)
            {
                accounts[index] = SnakeGame.CurrentUser;
                saveAccountsToFile(accounts);
                //SnakeGame.CurrentUser = accounts[index];
                return userId;
            }
            return -1;
        }
    }
}
