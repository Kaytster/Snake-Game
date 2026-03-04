using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Assessment
{
    //Storing the points collected in the game.
    class Points
    {
        protected int points;

        public Points()
        {
            points = 0;
        }

        public int Score
        {
            get { return points; }
        }

        public void AddPoint() //Add points for when snake eats a fruit
        {
            points++;
        }
    }

}
