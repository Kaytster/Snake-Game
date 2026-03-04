using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment
{
    //THE SNAKE
    public enum Direction {Up, Down, Left, Right}
    class Snake
    {
        protected List<Point> body;
        public Direction direction;
        protected const char Segment = '■';
        private bool growth;

        public ConsoleColor SnakeColour { get; set; } = ConsoleColor.Green; //Set default colour

        public Snake(Point startPosition, Direction startDirection) //Set start position and direction
        {
            body = new List<Point> { startPosition };
            direction = startDirection;
            growth = false;
        }

        public void changeDirection (Direction newDirection) //Prevents the snake going back on itself, as this would end the game
        {
            bool isOpposite =
                (direction == Direction.Up && newDirection == Direction.Down) ||
                (direction == Direction.Down && newDirection == Direction.Up) ||
                (direction == Direction.Left && newDirection == Direction.Right) ||
                (direction == Direction.Right && newDirection == Direction.Left);
            if (!isOpposite)
            {
                direction = newDirection;
            }
        }

        public void grow()
        {
            growth = true;
        }

        public void movement()
        {
            Point head = body[0]; //The first segment of the snake ([0] in the list) is the head of the snake
            Point newSegment;

            switch (direction) //Moving the coordinate of the snake depending on the direction used
            {
                case Direction.Up: newSegment = new Point(head.X, head.Y - 1); break;
                case Direction.Down: newSegment = new Point(head.X, head.Y + 1); break;
                case Direction.Left: newSegment = new Point(head.X -1, head.Y); break;
                case Direction.Right: newSegment = new Point(head.X +1, head.Y); break;
                default: throw new InvalidOperationException("Invalid Direction");
            }

            body.Insert(0, newSegment);

            if (!growth)
            {
                Point tail = body.Last();
                Console.SetCursorPosition(tail.X, tail.Y);
                Console.Write(' ');

                body.RemoveAt(body.Count - 1);
            }
            else
            {
                growth = false;
            }

            if (body.Skip(1).Any(segment => segment.X == newSegment.X && segment.Y == newSegment.Y))
            {
                Console.Clear();
                Console.WriteLine("GAME OVER");
            }
        }   

        public void drawSnake()
        {
            Point head = body[0];
            Console.SetCursorPosition(head.X, head.Y);
            //Console.ForegroundColor = this.SnakeColour;

            //Set the colour to the equipped skin
            Console.ForegroundColor = Rewards.UnlockRewards.SkinColours.GetColour(SnakeGame.CurrentUser.GetEquippedSnakeSkin());
            Console.Write(Segment);
            Console.ResetColor();

            if (body.Count > 1)
            {
                Point newBodySegment = body[1];
                Console.SetCursorPosition(newBodySegment.X, newBodySegment.Y);
                Console.ForegroundColor = this.SnakeColour;
                Console.Write(Segment);
            }
        }

        public Point Head => body[0];
        public int Length => body.Count;
        public List<Point> Body => body;
    }



    public struct Point
    {
        public int X { get; set; } 
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
