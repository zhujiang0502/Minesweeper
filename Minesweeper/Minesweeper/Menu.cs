using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Menu
    {
        public static string MessageAndInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public static string MessageForSquareRows(int numOfRow)
        {
            Console.WriteLine("\r\nplease input values for the row {0}\r\n" +
                              "    mines: \"*\"     no mines: \".\"\r\n" +
                              "    note: correct length for the row", numOfRow + 1);
            return Console.ReadLine();
        }

        public static void PrintResult(int rows, int columns, Square[,] squares)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write("\r\n");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(squares[i, j].ToString());
                }
            }
            Console.Write("\r\n");
        }



    }
}
