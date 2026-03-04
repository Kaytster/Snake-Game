namespace Programming_Assessment
{
    using System;
    using System.Linq;
    using System.Drawing;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using System.Numerics;
    using System.Threading.Tasks.Sources;
    using Programming_Assessment.Accounts;
    using System.Linq.Expressions;


    //List<T> - basic list
    //HashSet - <T> any order, no duplicates
    //Dictionary - <K, V> Key:Value pairs, e.g. <Int, String> (12345, hello)

    //SNAKE GAME
    //Accounts (multiple users) Tier and highscore included - DONE
    //Points system - DONE
    //High Score - DONE
    //Rewards (unlockable with points) - DONE Tier specific rewards too.
    //Leaderboard - Overall and clan leaderboard. - DONE Maybe include tier specific leaderboards as well.

    //FEEDBACK
    //  Could have tiers for users (for inheritance and polymorphism) like bronze silver gold.
    //  Could also have something like clans for users to work together.
    //  Add some specific try catch exceptions instead of just using a general one to handle more errors.

    internal class SnakeGame
    {
        public static bool isLoggedIn = false; //For checking if the user is logged in.
        public static UserAccount CurrentUser { get;  set; } //Holds the currently logged in user

        //private const string accountsFile = "Accounts.txt";
        public const string accountsFile = "Accounts.dat"; //File to hold the accounts
        private static void createFile() // Creating the account file
        {
            try
            {
                //Checking if the file exists
                if (!File.Exists(accountsFile))
                {
                    //Console.WriteLine("This file already exists");
                }

                //Creating the file
                using (FileStream fs = File.Create(accountsFile))
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes("Accounts");
                    //fs.Write(title, 0, title.Length);
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
        
        public static List<UserAccount> ReadAllAccounts() //Go through the accounts file and read all the accounts
        {
            //List<Accounts> accounts = new List<Accounts>();
            List<UserAccount> accounts = new List<UserAccount>();
            if (!File.Exists(accountsFile)) return accounts;

            try
            {
                using (FileStream fs = new FileStream(accountsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (br.BaseStream.Position < br.BaseStream.Length) //Writes the ID, Username, Password, Highscore, Equipped Snake, Equipped Fruit, and Clan
                    {
                        int id = br.ReadInt32();
                        string username = br.ReadString();
                        string password = br.ReadString();
                        int highscore = br.ReadInt32();

                        List<string> unlockedSkins = new List<string>();
                        int skinCount = br.ReadInt32();
                        for (int i = 0; i < skinCount; i++)
                        {
                            unlockedSkins.Add(br.ReadString());
                        }

                        string equippedSnakeSkin = br.ReadString();
                        string eqippedFruitSkin = br.ReadString();
                        string clan = br.ReadString();

                        //accounts.Add(new UserAccount(id, username, password, highscore));
                        UserAccount newAccount = new UserAccount(id, username, password, highscore, equippedSnakeSkin, eqippedFruitSkin, clan);
                        newAccount.InitializeSkins(unlockedSkins);
                        accounts.Add(newAccount);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading accounts file");
            }
            return accounts;
        }


        static void Main(string[] args)
        {
            if (args.Length == 2) //If 2 args have been provided run the login function, then start the app
            {
                Accounts.LoginAccount.logIn(args);
                gameIntro();
                Console.WriteLine("LOADING...");
                Thread.Sleep(3000);

            }
            else if (args.Length == 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("LOGIN INCOMPLETE");
                Console.ResetColor();
            }
            else if (args.Length == 0) // Starting the app without an account
            {
                isLoggedIn = false;
                gameIntro();
                Console.WriteLine("LOADING...");
                Thread.Sleep(3000);
            }

            //gameIntro();
            //Console.WriteLine("LOADING...");
            //Thread.Sleep(3000);

            bool appRunning = true;
            string nextAction = "MENU";

            while (appRunning) //Setting the action key for each menu and its functionality
            {
                //Console.WriteLine($"\n[DEBUG] The loop just started. nextAction is: '{nextAction}'");

                //Console.WriteLine("Press any key to continue to the next menu...");
                //Console.ReadKey(true);

                if (nextAction == "MENU") //Go to Main Menu
                {
                    nextAction = new Menus.MainMenu().ShowMenu();
                }
                else if (nextAction == "LEADERBOARD") //Go to Leaderboard
                {
                    nextAction = new Leaderboard().ShowMenu();
                }
                else if (nextAction == "ACCOUNT") //Go to Account Menu
                {
                    nextAction = new Menus.AccountMenu().ShowMenu();
                }
                else if (nextAction == "REWARDS") //Go to Rewards Menu
                {
                    nextAction = new Menus.RewardsMenu().ShowMenu();
                }
                else if (nextAction == "EDIT") //Go to Edit Account Menu
                {
                    nextAction = new Accounts.EditAccount().ShowMenu();
                }
                else if (nextAction == "CREATE") //Run Create Account Function
                {
                    nextAction = Accounts.CreateAccount.createAccount();
                }
                else if (nextAction == "SNAKE") //Go to Snake Skins Menu
                {
                    nextAction = new Rewards.SnakeSkins().ShowMenu();
                }
                else if (nextAction.StartsWith("S_")) //Check if the action starts with "S_". If true - Run Unlock Snake Skin Function
                {
                    string sName = nextAction.Substring(2);
                    if (Rewards.SnakeSkins.SkinData.Cost.TryGetValue(sName, out int costS))
                    {
                        nextAction = new Rewards.UnlockRewards().UnlockSkinSnake(sName, costS);
                    }
                }
                else if (nextAction == "FRUIT") //Go to Fruit Skins Menu
                {
                    nextAction = new Rewards.FruitSkins().ShowMenu();
                }
                else if (nextAction.StartsWith("F_")) //Check if the action starts with "F_". If true - Run Unlock Fruit Skin Function
                {
                    
                    string fName = nextAction.Substring(2);

                    if (Rewards.FruitSkins.SkinData.Cost.TryGetValue(fName, out int costF))
                    {
                        nextAction = new Rewards.UnlockRewards().UnlockSkinFruit(fName, costF);
                    }
                }
                else if (nextAction == "EQUIP") //Go to the Snake Skins Menu
                {
                    nextAction = "SNAKE";
                }
                //if (nextAction == "UNLOCKED")
                //{
                //    new Rewards.UnlockRewards().unlockMessagesSnake("Skin Unlocked Successfully!", 2000);
                //    new Rewards.UnlockRewards().unlockMessagesFruit("Skin Unlocked Successfully!", 2000);

                //    nextAction = "SNAKE";
                //}
                else if (nextAction == "UNAVAILABLE") //Go to the Snake Skins Menu
                {
                    nextAction = "SNAKE";
                }
                else if (nextAction == "USERNAME") 
                {
                    nextAction = new Menus.MainMenu().ShowMenu();
                }
                else if (nextAction == "PASSWORD")
                {
                    nextAction = new Menus.MainMenu().ShowMenu();
                }
                else if (nextAction == "CLAN") //Go to Clan Menu
                {
                    nextAction = new Menus.ClanMenu().ShowMenu();
                }
                else if (nextAction.StartsWith("CLAN_")) //Check if the action starts with "CLAN_". If true - Set Clan Names, Set User Clan
                {
                    string clanKey = nextAction.Substring(5);
                    string fullClanName = "";

                    switch (clanKey)
                    {
                        case "V": fullClanName = "The VIPERS"; break;
                        case "C": fullClanName = "The COBRAS"; break;
                        case "P": fullClanName = "The PYTHONS"; break;
                        case "T": fullClanName = "The TITANS"; break;
                        case "M": fullClanName = "The MAMBAS"; break;
                    }

                    if (CurrentUser != null)
                    {
                        CurrentUser.Clan = fullClanName;
                        int result = Accounts.SaveAccount.saveUpdatedAccount();
                        if (result != -1)
                        {
                            Console.WriteLine($"\nSuccessfully joined {fullClanName}!");
                        }
                        else
                        {
                            Console.WriteLine("\nError: Could not save clan selection.");
                        }
                        Thread.Sleep(2000);
                    }
                    nextAction = "ACCOUNT";
                }
                else if (nextAction == "RESTART" || nextAction == "START") //Restart the Game
                {
                    nextAction = gameArea();
                }
                else if (nextAction == "EXIT") //Exit the Game
                {
                    appRunning = false;
                }
                else
                {
                    nextAction = "MENU";
                }
                //if (nextAction != "MENU" && nextAction != "RESTART" && nextAction != "EXIT"
                //    && nextAction != "LEADERBOARD" && nextAction != "ACCOUNT" && nextAction != "REWARDS"
                //    && nextAction != "EDIT" && nextAction != "CREATE" && nextAction != "SNAKE"
                //    && nextAction != "FRUIT" && nextAction != "OWNED" && nextAction != "UNLOCKED"
                //    && nextAction != "UNAVAILABLE" && nextAction != "USERNAME" && nextAction != "PASSWORD"
                //    && !nextAction.StartsWith("S_")
                //    && !nextAction.StartsWith("F_"))
                //{
                //    nextAction = "MENU";
                //}
            }
                //ConsoleRectangle obj = new ConsoleRectangle(40, 20, new Point(1, 1), ConsoleColor.Cyan);
                //obj.Draw(); //40w 20h
        }
        public static void gameIntro() //Checking if the user has started the app
        {
            gameTitleText();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Press ENTER to begin...");
            var startGame = ConsoleKey.Enter;
            while (true)
            {
                var userInput = Console.ReadKey(true);

                if (userInput.Key == startGame)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please press ENTER to begin...");
                }
                Console.Clear();
            }
        }

        public static string gameArea()
        {
            Console.Clear();

            //Drawing the border of the game
            ConsoleRectangle obj = new ConsoleRectangle(40, 20, new Point(1, 1), ConsoleColor.Cyan);
            obj.Draw(); //40w 20h
            Console.Write("Points: ");

            int width = 40;
            int height = 20;
            Point startCorner = new Point(1, 1);
            
            //Setting default snake colour, fruit colour, and fruit char
            ConsoleColor snakeColour = ConsoleColor.Green;
            char fruitIcon = '0';
            ConsoleColor fruitColour = ConsoleColor.Red;

            //If user is logged in - get the equipped fruit and snake
            if (isLoggedIn && CurrentUser != null)
            {
                string equippedSnake = CurrentUser.GetEquippedSnakeSkin();
                snakeColour = Rewards.UnlockRewards.SkinColours.GetColour(equippedSnake);

                string equippedFruit = CurrentUser.GetEquippedFruitSkin();
                fruitIcon = Rewards.UnlockRewards.SkinColours.GetChar(equippedFruit);
                fruitColour = Rewards.UnlockRewards.SkinColours.GetColour(equippedFruit);
            }
            //Set the snake and fruit to the equipped skins
            Snake s = new Snake(new Point(10, 10), Direction.Right);
            s.SnakeColour = snakeColour;
            RandomFruit f = new RandomFruit();
            f.icon = fruitIcon;
            f.FruitColour = fruitColour;
            Points p = new Points();

            bool isRunning = true;
            int gameSpeedMs = 150;
            Direction inputDirection = s.direction;
            generateFruit(f, s, width, height, startCorner);

            while (isRunning)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Reads the key and uses it in getDirection
                        inputDirection = getDirection(keyInfo, s.direction);
                    }

                    //This is then put into changeDirection, to make sure the snake can't go back on itself
                    s.changeDirection(inputDirection);

                    
                    s.movement(); //The movement of the snake

                    if(isCollision(s.Head, width, height, startCorner)) //When the snake collides with the border of the game
                    {
                        isRunning = false;
                        return gameOverScreen(p);
                        
                    }

                    if(s.Head.X == f.Location.X && s.Head.Y == f.Location.Y)
                    {
                        s.grow();
                        p.AddPoint();
                        generateFruit(f, s, width, height, startCorner);
                    }

                    //Creating the snake
                    s.drawSnake();
                    f.Draw();
                    pointDisplay(p.Score, startCorner.X, startCorner.Y + height + 2);

                    Thread.Sleep(gameSpeedMs);
                }
                catch (Exception ex) when (ex.Message == "SelfCollision")
                {
                    
                    isRunning = false;

                    Console.WriteLine("GAME OVER - Self Collision!");
                    return gameOverScreen(p);
                    
                }
                catch (Exception ex)
                {
                    
                    isRunning = false;
                    
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    return "MENU";
                }
            }
            return "MENU";
        }

        //GAME FUNCTIONS
        public static Direction getDirection(ConsoleKeyInfo keyInfo, Direction currentDirection) //Setting the direction of the snake using the arrow keys
        {
            Direction newDirection = currentDirection;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: newDirection = Direction.Up; break;
                case ConsoleKey.DownArrow: newDirection = Direction.Down; break;
                case ConsoleKey.LeftArrow: newDirection = Direction.Left; break;
                case ConsoleKey.RightArrow: newDirection = Direction.Right; break;
            }
            return newDirection;
        }

        public static bool isCollision(Point head, int width, int height, Point startCorner) //Checking if the snake collided with the walls of the game
        {
            //LEFT WALL
            if (head.X == startCorner.X)
            {
                return true;
            }
            //RIGHT WALL
            if (head.X == startCorner.X + width + 1)
            {
                return true;
            }
            //TOP WALL
            if (head.Y == startCorner.Y)
            {
                return true;
            }
            //BOTTOM WALL
            if (head.Y == startCorner.Y + height + 1)
            {
                return true;
            }

            return false;
        }

        public static void generateFruit(RandomFruit f, Snake s, int width, int height, Point startCorner) //Generating the random fruit
        {
            f.GetRandomCoordinate(startCorner.X, startCorner.Y, width, height, s.Body);
        }

        public static void pointDisplay(int score, int displayX, int displayY) //Displaying the users points during the game
        {
            Console.SetCursorPosition(displayX + 8, displayY);
            Console.Write("   ");
            Console.SetCursorPosition(displayX + 7, displayY);
            Console.Write(score);
        }


        //TEXT FUNCTIONS
        public static void gameTitleText() //Creating the cool title.
        {
            string[] title = new string[] //Setting the title as an array of strings.
{
             "         ▒▒▒▒▒▒▒▒▒▒▒   ▒▒▒▒▒    ▒▒▒▒▒      ▒▒▒▒▒▒▒▒      ▒▒▒▒▒    ▒▒▒▒▒   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒ ",
             "      ▒▒▒▒█████████▒   ▒███▒▒▒▒ ▒███▒   ▒▒▒▒██████▒▒▒▒   ▒███▒ ▒▒▒▒███▒   ▒████████████▒ ",
             "      ▒███▒▒▒▒▒▒▒▒▒▒   ▒██████▒▒▒███▒   ▒███▒▒▒▒▒▒███▒   ▒███▒▒▒███▒▒▒▒   ▒███▒▒▒▒▒▒▒▒▒▒ ",
             "      ▒▒▒▒██████▒▒▒▒   ▒███▒▒▒██████▒   ▒████████████▒   ▒██████▒▒▒▒      ▒█████████▒    ",
             "      ▒▒▒▒▒▒▒▒▒▒███▒   ▒███▒ ▒▒▒▒███▒   ▒███▒▒▒▒▒▒███▒   ▒███▒▒▒███▒▒▒▒   ▒███▒▒▒▒▒▒▒▒▒▒ ",
             "      ▒▒████████▒▒▒▒   ▒███▒    ▒███▒   ▒███▒    ▒███▒   ▒███▒ ▒▒▒▒███▒   ▒████████████▒ ",
             "      ▒▒▒▒▒▒▒▒▒▒▒      ▒▒▒▒▒    ▒▒▒▒▒   ▒▒▒▒▒    ▒▒▒▒▒   ▒▒▒▒▒    ▒▒▒▒▒   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒ "
};
            char fillChar = '█';
            char hightlightChar = '▒';
            ConsoleColor fillColor = ConsoleColor.Green;
            ConsoleColor highlightColor = ConsoleColor.Blue;
            ConsoleColor defaultColor = ConsoleColor.White;
            Console.WriteLine("\n\n");
            foreach (string line in title)
            {
                printArt(line, fillChar, fillColor, hightlightChar, highlightColor, defaultColor);
            }
            Console.WriteLine("\n\n");
            //Console.WriteLine(title);
        }
        public static void gameTitleTextColour(string title, char fillChar, char highlightChar, ConsoleColor fillColor, ConsoleColor highlightColor, ConsoleColor defaultColor) //Setting the colours for the title
        {
            Console.ForegroundColor = defaultColor;
            foreach (char character in title)
            {
                if (character == fillChar)
                {
                    Console.ForegroundColor = fillColor;
                }
                else if (character == highlightChar)
                {
                    Console.ForegroundColor = highlightColor;
                }
                else
                {
                    Console.ForegroundColor = defaultColor;
                }
            }
        }
        public static void printArt(string title, char char1, ConsoleColor color1, char char2, ConsoleColor color2, ConsoleColor defaultColor) //Printing the title
        {
            Console.ForegroundColor = defaultColor;
            foreach (char character in title)
            {
                if (character == char1)
                {
                    Console.ForegroundColor = color1;
                }
                else if (character == char2)
                {
                    Console.ForegroundColor = color2;
                }
                else
                {
                    Console.ForegroundColor = defaultColor;
                }
                Console.Write(character);
            }
            Console.WriteLine();
            Console.ForegroundColor = defaultColor;
        }

        public static string gameOverScreen(Points finalScore) //Game over screen
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            string gameOverText = @"
   _____          __  __ ______    ______      ________ _____  
  / ____|   /\   |  \/  |  ____|  / __ \ \    / /  ____|  __ \ 
 | |  __   /  \  | \  / | |__    | |  | \ \  / /| |__  | |__) |
 | | |_ | / /\ \ | |\/| |  __|   | |  | |\ \/ / |  __| |  _  / 
 | |__| |/ ____ \| |  | | |____  | |__| | \  /  | |____| | \ \ 
  \_____/_/    \_\_|  |_|______|  \____/   \/   |______|_|  \_\
                                                               
                ";
            Console.WriteLine(gameOverText);
            Console.ResetColor();

            int score = finalScore.Score;
            Console.Write("Total Score: ");
            Console.Write($"{score} "); //show total score

            try
            {
                //If the user is logged in show their high score, and if they beat it show their new high score.
                if (isLoggedIn && CurrentUser != null)
                {
                    if (score > CurrentUser.GetHighScore())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"* NEW HIGH SCORE: {score} *");
                        Console.ResetColor();
                        
                        //Update their account with the new high score
                        CurrentUser.SetHighScore(score);
                        List<UserAccount> allAccounts = ReadAllAccounts();
                        UserAccount accountToUpdate = allAccounts.FirstOrDefault(acc => acc.GetID() == CurrentUser.GetID());
                        if (accountToUpdate != null)
                        {
                            accountToUpdate.SetHighScore(score);
                        }
                        Accounts.SaveAccount.saveAccountsToFile(allAccounts);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"* HIGH SCORE: {CurrentUser.GetHighScore()} *");
                        Console.ResetColor();
                        
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nCRITICAL ERROR in High Score logic: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\n\nPress [ENTER] to play again or [X] to return to the MAIN MENU");
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    string action = "RESTART";
                    return "RESTART";
                }
                else if (key.Key == ConsoleKey.X)
                {
                    string action = "MENU";
                    return "MENU";
                }
            }

            

        }



    }
}

