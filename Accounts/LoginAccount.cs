using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment.Accounts
{
    class LoginAccount
    {
        //Logging into an existing account
        public static void logIn(string[] args)
        {
            string username = null;
            string password = null;

            if (args.Length == 2) //Checking if the right args have been provided
            {
                //Console.WriteLine("username and password input...");
                username = args[0];
                password = args[1];
                //Console.WriteLine($"attempting to login with {username} + {password}...");
                //Console.WriteLine("attempt complete");
                SnakeGame.isLoggedIn = true;
            }
            else
            {
                Console.WriteLine("username and password input...");
                Console.WriteLine("attempt incomplete");
            }

            try
            {
                List<UserAccount> accounts = SnakeGame.ReadAllAccounts();
                //Check for a matching account
                UserAccount matchingAccount = accounts.FirstOrDefault(acc =>
                    acc.GetUsername().Equals(username, StringComparison.OrdinalIgnoreCase) &&
                    acc.GetPassword() == password); 

                if (matchingAccount != null) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Welcome back, {matchingAccount.GetUsername()}!");
                    SnakeGame.isLoggedIn = true;
                    SnakeGame.CurrentUser = matchingAccount;
                    Console.ResetColor();
                    Thread.Sleep(3000);
                    //new Menus.MainMenu().ShowMenu();
                    //SnakeGame.gameIntro();
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Accounts file not found.");
                SnakeGame.isLoggedIn = false;
                Console.ResetColor();
                Thread.Sleep(3000);
                new Menus.AccountMenu().ShowMenu();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Unexpected Error");
                SnakeGame.isLoggedIn = false;
                Console.ResetColor();
                Thread.Sleep(3000);
                new Menus.AccountMenu().ShowMenu();
            }
        }
    }
}
