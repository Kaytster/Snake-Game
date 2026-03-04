using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment
{
    //Setting the random fruit
    class RandomFruit
    {
        protected int X;
        protected int Y;
        protected string name;
        //protected char icon;
        public ConsoleColor FruitColour { get; set; } = ConsoleColor.Red;
        public char icon { get; set; } = '0';

        public RandomFruit()
        {
            //X = initialX;
            //Y = initialY;
            //name = initialName;
            icon = '0';
            FruitColour = ConsoleColor.Red;
            //Set default char and colour
        }

        public Point Location
        {
            get
            {
                return new Point(X, Y);
            }
        }

        public void GetRandomCoordinate(int startX, int startY, int innerWidth, int innerHeight, List<Point> Body)
        {
            //Get the random location of the fruit using RANDOM
            Point newLocation;
            int randomCoordX;
            int randomCoordY;
            Random rnd = new Random();
            do
            {
                //Make sure it is in the area of the game border
                 randomCoordX = rnd.Next(startX + 1, startX + innerWidth);
                 randomCoordY = rnd.Next(startY + 1, startY + innerHeight);
                newLocation = new Point(randomCoordX, randomCoordY);
            }
            while (Body.Contains(newLocation));
            //int randomCoord1 = rnd.Next(40, 20);
            //int randomCoord2 = rnd.Next(40, 20);
            X = randomCoordX;
            Y = randomCoordY;
        }

        public void Draw() //Drawing the fruit
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = FruitColour;
            Console.Write(icon);
            Console.ResetColor();
        }

        //public void SetPostition(int initialX, int initialY)
        //{
        //    int newX = GetRandomCoordinate();
        //}
    }
}
