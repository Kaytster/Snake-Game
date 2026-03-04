using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment
{
    //Setting the rectangle that will be the border of the game
    //I used this YouTube Tutorial to help me write this code:
    // https://www.youtube.com/shorts/dv5azSfMVqk
    class ConsoleRectangle
    {
        protected int hWidth;
        protected int hHeight;
        protected Point hLocation;
        protected ConsoleColor hBorderColor;

        public ConsoleRectangle(int width, int height, Point location, ConsoleColor borderColor)
        {
            hWidth = width;
            hHeight = height;
            hLocation = location;
            hBorderColor = borderColor;
        }

        public Point Location
        {
            get { return hLocation; }
            set { hLocation = value; }
        }

        public int Width
        {
            get { return hWidth; }
            set { hWidth = value; }
        }

        public int Height
        {
            get { return hHeight; }
            set { hHeight = value; }
        }

        public ConsoleColor BorderColor
        {
            get { return hBorderColor; }
            set { hBorderColor = value; }
        }

        public void Draw()
        {
            string s = "┌"; // Top left
            string space = "";
            string temp = "";
            for (int i = 0; i < Width; i++)
            {
                space += " ";
                s += "─";
            }

            for (int j = 0; j < Location.X; j++)
                temp += " ";

            s += "┐" + "\n"; // Top right

            for (int i = 0; i < Height; i++)
                s += temp + "│" + space + "│" + "\n";

            s += temp + "└"; //Bottom left
            for (int i = 0; i < Width; i++)
                s += "─";

            s += "┘" + "\n"; //Bottom right

            Console.ForegroundColor = BorderColor;
            Console.CursorTop = hLocation.Y;
            Console.CursorLeft = hLocation.X;
            Console.Write(s);
            Console.ResetColor();

            //ConsoleRectangle obj =
            //    new ConsoleRectangle(10, 10, new Point(6), ConsoleColor.Red);
            //obj.Draw();

        }
    }
}
