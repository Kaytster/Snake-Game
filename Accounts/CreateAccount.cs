using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    //Creating a new account
    class CreateAccount
    {
        //Creates a unique userID for a new account
        public static int createUserID()
        {
            int IDnumber = 0;
            try
            {
                //Reading how many accounts are in the file to get the next number
                IDnumber = SnakeGame.ReadAllAccounts().Count;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Cannot read the Accounts file");
                Console.ResetColor();
                return 1;
            }
            return IDnumber + 1;
        }

        //Creating a new account
        public static string createAccount()
        {
            Console.WriteLine("Please enter a username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter a password: ");
            string password = Console.ReadLine();
            int userID = createUserID(); //Getting a user ID from the generator

            //Accounts newAccount = new Accounts(userID, username, password);
            UserAccount newAccount = new UserAccount(userID, username, password, 0, "LIME", "APPLE", "None");

            try
            {
                //Add the account tp the file and then save
                List<UserAccount> accounts = SnakeGame.ReadAllAccounts();
                accounts.Add(newAccount);
                SaveAccount.saveAccountsToFile(accounts);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Welcome {username}! Your account has been created successfully");
                SnakeGame.isLoggedIn = true;
                SnakeGame.CurrentUser = newAccount;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Redirecting");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.ResetColor();
                //new Menus.MainMenu().ShowMenu();
                return "MENU";
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Account could not be created");
                SnakeGame.isLoggedIn = false;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Redirecting");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.ResetColor();
                //new Menus.MainMenu().ShowMenu();
                return "MENU";
            }
        }
    }
}
