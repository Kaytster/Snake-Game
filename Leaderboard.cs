using Programming_Assessment.Accounts;
using Programming_Assessment.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment
{
    //Displays the Overall Leaderboard and Clan Leaderboard for the Game.
    class Leaderboard : MenuBase
    {
        protected static List<UserAccount> allAccounts = new List<UserAccount>();
        
        public Leaderboard()
        {
            if (allAccounts == null || allAccounts.Count == 0)
            {
                allAccounts = SnakeGame.ReadAllAccounts();
            }
        }
        public static void DisplayOverall() //Overall Leaderboard
        {
            int userHighScore = SnakeGame.CurrentUser.GetHighScore();
            int baseHighScore = 0; //Get the Users High Score

            //List<UserAccount> topUsers = allAccounts.OrderByDescending(account => account.GetHighScore()).Take(10).ToList();

            //Using AsParallel for efficiency. Get the top 10 users from the accounts file.
            List<UserAccount> topUsers = allAccounts.AsParallel().OrderByDescending(account => account.GetHighScore()).Take(10).ToList();
            int rank = 1; //Start rank 1

            foreach (var user in topUsers)
            {
                //Get rank, username, highscore
                string rankString = rank.ToString();
                string userString = user.GetUsername();
                string scoreString = user.GetHighScore().ToString();

                string line = $"|{rankString} | {userString} | {scoreString}|";

                if (user.GetUsername().Equals(SnakeGame.CurrentUser.GetUsername()))
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(line);
                    Console.WriteLine("--------------------");
                }
                else
                {
                    Console.WriteLine(line);
                }
                rank++;
            }
            
        }

        protected override void Draw(int selectedIndex) //Drawing the Leaderboard
        {
            string titleText = @"

                 _                   _           _                         _ 
                | |    ___  __ _  __| | ___ _ __| |__   ___   __ _ _ __ __| |
                | |   / _ \/ _` |/ _` |/ _ \ '__| '_ \ / _ \ / _` | '__/ _` |
                | |__|  __/ (_| | (_| |  __/ |  | |_) | (_) | (_| | | | (_| |
                |_____\___|\__,_|\__,_|\___|_|  |_.__/ \___/ \__,_|_|  \__,_|

             ";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(titleText);
            Console.ResetColor();
            Console.WriteLine("---OVERALL TOP 10---");
            DisplayOverall();
            Console.WriteLine("---CLAN TOP 5---");
            DisplayClan();
            Items = new List<MenuItems>
            {
                new MenuItems("BACK", null, "MENU")
            };
        }

        public static void DisplayClan() //Clan Leaderboard
        {
            //Using AsParallel for efficiency. Get the clans
            var clanLeaderboard = allAccounts.AsParallel().Where(a => !string.IsNullOrEmpty(a.Clan) && a.Clan != "None")
                                             .GroupBy(a => a.Clan)
                                             .Select(group => new
                                             {
                                                 ClanName = group.Key,
                                                 TotalScore = group.Sum(a => a.GetHighScore()),
                                                 MemberCount = group.Count()
                                             })
                                             .OrderByDescending(c => c.TotalScore)
                                             .ToList();
            if (clanLeaderboard.Count == 0)
            {
                Console.WriteLine("There are no Clans yet"); //If no clans have been formed yet
                return;
            }

            foreach (var clan in clanLeaderboard)
            {
                string line = string.Format("| {0,-15} | Score: {1,-8} | Members: {2,-3} |",
                            clan.ClanName, clan.TotalScore, clan.MemberCount); //Show Name, Score, and Members

                if (SnakeGame.isLoggedIn && clan.ClanName == SnakeGame.CurrentUser.Clan)
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(line);
                    Console.WriteLine("--------------------");
                }
                else
                {
                    Console.WriteLine(line);
                }
            }

        }

        //public class Top10
        //{
        //    public string username { get; set; }
        //    public string highscore { get; set; }
        //}
    }
}
