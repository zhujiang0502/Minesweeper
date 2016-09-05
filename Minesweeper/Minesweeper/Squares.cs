using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Minesweeper
{
    class Squares
    {
        private Square[,] squares;

        public Squares(int x, int y)
        {
            squares = new Square[x, y];
        }

        //the method create a mine with "*" input and a unmined with "."
        //difference is that the unmined is given number 0 as its initial value representing the number of adjacent mines
        public void Add(int x, int y, char value)
        {
            if (value == '*') squares[x, y] = new Mine(value);
            else squares[x, y] = new Unmined(0);
        }

        //method to print out value of mines and number of adjacent mines of the unmineds
        public Square[,] GetResultedSquares()
        {
            return squares;
        }

        //main method which utilizes several other methods to implement calculation for adjacent mines
        public void countMines(int rows, int columns)
        {
            //the method does nothing with special situation that both rows and columns equal to 1
            if (rows == 1 && columns == 1)
            { }

            //for other situations
            else
            {
                for (int x = 0; x <= rows - 1; x++)
                {
                    for (int y = 0; y <= columns - 1; y++)
                    {
                        //the method starts calculating only when a mine is found
                        if (squares[x, y] is Mine)
                        {
                            if (rows == 1 || columns == 1) RowOrColumnSmallerThan2(x, y, rows, columns);
                            else RowAndColumnBiggerThan2(x,y,rows,columns);
                        }
                    }
                }
            }
        }

        //method is called when only one of row or column is 1, which means the shape of a horizontal or vertical line of squares
        private void RowOrColumnSmallerThan2(int x, int y, int rows, int columns)
        {
            //horizontal line of squares
            if (rows == 1)
            {
                if (!CornersCheck(x, y, rows, columns) && !EdgeCheck(x, y, rows, columns))
                {
                    squares[x, y + 1].AddNumOfAdjacentMines();
                    squares[x, y - 1].AddNumOfAdjacentMines();
                }
            }

            //vertical line of squares
            else if (columns == 1)
            {
                if (!CornersCheck(x, y, rows, columns) && !EdgeCheck(x, y, rows, columns))
                {
                    squares[x + 1, y].AddNumOfAdjacentMines();
                    squares[x - 1, y].AddNumOfAdjacentMines();
                }
            }
        }

        //method is called when both row and column are equal or over 2,
        private void RowAndColumnBiggerThan2(int x, int y, int rows, int columns)
        {
            if (!CornersCheck(x, y, rows, columns) && !EdgeCheck(x, y, rows, columns))
            {
                squares[x + 1, y].AddNumOfAdjacentMines();
                squares[x, y + 1].AddNumOfAdjacentMines();
                squares[x + 1, y + 1].AddNumOfAdjacentMines();
                squares[x + 1, y - 1].AddNumOfAdjacentMines();
                squares[x, y - 1].AddNumOfAdjacentMines();
                squares[x - 1, y - 1].AddNumOfAdjacentMines();
                squares[x - 1, y].AddNumOfAdjacentMines();
                squares[x - 1, y + 1].AddNumOfAdjacentMines();
            }
        }

        //method to locate neighbors of four corners
        private bool CornersCheck(int x, int y, int rows, int columns)
        {
            //left top corner
            if (x == 0 & y == 0)
            {
                //horizontal, vertical, and other shapes will have different checks for neighboring square
                if (rows == 1) squares[x, y + 1].AddNumOfAdjacentMines();
                else if (columns == 1) squares[x + 1, y].AddNumOfAdjacentMines();
                else if (rows != 1 && columns != 1)
                {
                    squares[x, y + 1].AddNumOfAdjacentMines();
                    squares[x + 1, y].AddNumOfAdjacentMines();
                    squares[x + 1, y + 1].AddNumOfAdjacentMines();
                }
                return true;
            }
            //right top corner
            if (x == 0 && y == columns - 1)
            {
                squares[x, y - 1].AddNumOfAdjacentMines();
                if (rows != 1)
                {
                    squares[x + 1, y].AddNumOfAdjacentMines();
                    squares[x + 1, y - 1].AddNumOfAdjacentMines();
                }
                return true;
            }
            //left bottom corner
            if (x == rows - 1 && y == 0)
            {
                squares[x - 1, y].AddNumOfAdjacentMines();
                if (columns !=1)
                {
                    squares[x, y + 1].AddNumOfAdjacentMines();
                    squares[x - 1, y + 1].AddNumOfAdjacentMines();
                    return true;
                }
            }
            //right bottom corner
            if (x == rows - 1 && y == columns - 1)
            {
                //this condition is set in case (columns - 1) equal to 0, which conflicts with left bottom corner
                if (columns != 1)
                {
                    squares[x - 1, y].AddNumOfAdjacentMines();
                    squares[x, y - 1].AddNumOfAdjacentMines();
                    squares[x - 1, y - 1].AddNumOfAdjacentMines();
                }
                return true;
            }
            return false;
        }

        //method to locate neighbors of a square on the edge
        private bool EdgeCheck(int x, int y, int rows, int columns)
        {
            //top edge
            if (x == 0 && y != 0 && y != columns - 1)
            {
                squares[x, y + 1].AddNumOfAdjacentMines();
                squares[x, y - 1].AddNumOfAdjacentMines();
                if (rows != 1) 
                {
                    squares[x + 1, y].AddNumOfAdjacentMines();
                    squares[x + 1, y + 1].AddNumOfAdjacentMines();
                    squares[x + 1, y - 1].AddNumOfAdjacentMines();
                }
                return true;
            }
            //left edge
            if (y == 0 && x != 0 && x != rows - 1)
            {
                squares[x - 1, y].AddNumOfAdjacentMines();
                squares[x + 1, y].AddNumOfAdjacentMines();
                if (columns != 1)
                {
                    squares[x, y + 1].AddNumOfAdjacentMines();
                    squares[x - 1, y + 1].AddNumOfAdjacentMines();
                    squares[x + 1, y + 1].AddNumOfAdjacentMines();
                }
                return true;
            }
            //bottom edge
            if (x == rows - 1 && y != 0 && y != columns - 1)
            {
                squares[x - 1, y].AddNumOfAdjacentMines();
                squares[x, y + 1].AddNumOfAdjacentMines();
                squares[x - 1, y + 1].AddNumOfAdjacentMines();
                squares[x, y - 1].AddNumOfAdjacentMines();
                squares[x - 1, y - 1].AddNumOfAdjacentMines();
                return true;
            }
            //right edge
            if (y == columns - 1 && x != 0 && x != rows - 1)
            {
                squares[x - 1, y].AddNumOfAdjacentMines();
                squares[x, y - 1].AddNumOfAdjacentMines();
                squares[x - 1, y - 1].AddNumOfAdjacentMines();
                squares[x + 1, y].AddNumOfAdjacentMines();
                squares[x + 1, y - 1].AddNumOfAdjacentMines();
                return true;
            }
            return false;
        }

    }
}
