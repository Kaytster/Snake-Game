using Programming_Assessment.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    //Menu for editing accounts:
    // -    Edit Username
    // -    Edit Password
    //This will not allow users to change their ID
    //Inherits from MenuBase
    class EditAccount : MenuBase
    {
        public EditAccount()
        {
            Title = "EDIT ACCOUNT";
            Items = new List<MenuItems>
            {
                new MenuItems("USERNAME", () => EditUsername(), "USERNAME"),
                new MenuItems("PASSWORD", () => EditPassword(), "PASSWORD")
            };
        }

        public static void EditUsername()
        {

            string getUser = SnakeGame.CurrentUser.GetUsername(); //Get current username

            string currentUsername;
            string newUsername;
            
            while (true)
            {
                //Verify current username
                Console.Write("Please enter your CURRENT username: ");
                currentUsername = Console.ReadLine();

                if (currentUsername == getUser)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Username INCORRECT");
                }

            }
            do
            {
                Console.Write("Please enter your NEW username: ");
                newUsername = Console.ReadLine();

                //Check if the username is already taken
                List<UserAccount> accounts = SnakeGame.ReadAllAccounts();
                bool doesAccountExist = accounts.AsParallel().Any(account => account.GetUsername().Equals(newUsername, StringComparison.OrdinalIgnoreCase));

                if (doesAccountExist)
                {
                    Console.WriteLine("This username is already taken.");
                }
                else
                {
                    SnakeGame.CurrentUser.SetUsername(newUsername);
                    SaveAccount.saveUpdatedAccount(); //Set Username and save to account
                    SnakeGame.isLoggedIn = true;
                    Console.WriteLine("Username updated successfully!");
                    Console.Write("Redirecting");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    new Menus.AccountMenu().ShowMenu();
                    break;
                }
            } while (true);
            
        }

        public static void EditPassword()
        {

            string getPass = SnakeGame.CurrentUser.GetPassword(); //Get current password

            string currentPassword;
            string newPassword;

            while (true)
            {
                //Verify current password
                Console.Write("Please enter your CURRENT password: ");
                currentPassword = Console.ReadLine();

                if (currentPassword == getPass)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("password INCORRECT");
                }

            }
            do
            {
                //Get new password
                Console.Write("Please enter your NEW password: ");
                newPassword = Console.ReadLine();

                List<UserAccount> accounts = SnakeGame.ReadAllAccounts();
                bool doesPasswordExist = accounts.Any(account => account.GetPassword().Equals(newPassword));

                if (doesPasswordExist)
                {
                    Console.WriteLine("New password cannot be the same as the old password");
                }
                else
                {
                    SnakeGame.CurrentUser.SetPassword(newPassword);
                    SaveAccount.saveUpdatedAccount(); //Set Password and save to account
                    SnakeGame.isLoggedIn = true;
                    Console.WriteLine("Password updated successfully!");
                    Console.Write("Redirecting");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    Console.Write(".");
                    Thread.Sleep(1000);
                    new Menus.AccountMenu().ShowMenu();
                    break;
                }
            } while (true);

        }
    }
}
