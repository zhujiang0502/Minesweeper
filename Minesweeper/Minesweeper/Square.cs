using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    abstract class Square
    {
        public virtual void AddNumOfAdjacentMines() { }
    }

    class Mine:Square
    {
        private char Value;

        public Mine(char input)
        {
            Value = input;
        }

        public override string ToString()
        {
            return String.Format("{0}",Value);
        }
    }

    class Unmined:Square
    {
        private int numOfAdjacentMines;

        public Unmined(int input)
        {
            numOfAdjacentMines = input;
        }

        public override string ToString()
        {
            return String.Format("{0}", numOfAdjacentMines);
        }

        //method will be used one time when a mine is found around
        public override void AddNumOfAdjacentMines()
        {
            numOfAdjacentMines += 1;
        }
    }
}
