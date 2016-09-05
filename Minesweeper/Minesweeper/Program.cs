using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            RunApplication();
        }

        //The whole process will be implemented in this method.
        private static void RunApplication()
        {
            bool runProgram = true;
            while (runProgram == true)
            {
                int rows = 0, columns = 0;
                string str = Menu.MessageAndInput("Please enter two integer numbers (1-99) as rows and columns\r\n" +
                       "with a blank between them");

                //A section to attempt to retrieve rows and columns from the string input
                //The process willbe repeated with wrong input.
                bool setRowsColumns = TestRowsAndColumnsInput(str, out rows, out columns);
                while (setRowsColumns == false)
                {
                    str = Menu.MessageAndInput("Invalid input! please enter two integer numbers (1-99) as rows and columns\r\n" +
                       "with a blank between them");
                    setRowsColumns = TestRowsAndColumnsInput(str, out rows, out columns);
                }

                //create squares with mines and the unmined, and then print out the result
                Squares s = ConfigureSquares(rows, columns);
                s.countMines(rows, columns);
                Menu.PrintResult(rows,columns,s.GetResultedSquares());

                string selection = Menu.MessageAndInput("\r\nenter \"r\" to run the program again, otherwise, enter any key to close the program"); 
                if (selection != "r") runProgram = false;
            }
        }

        //This method takes in number of rows and columns, and then try to generate the square with only right format of inputted values for each row
        private static Squares ConfigureSquares(int rows, int columns)
        {
            Squares s = new Squares(rows, columns);

            Console.WriteLine("\r\nplease input values for the row\r\n" +
                              "    mines: \"*\"     no mines: \".\"\r\n" +
                              "    note: correct length for the row");

            for (int x = 0; x < rows; x++)
            {
                //string value = Menu.MessageForSquareRows(x);

                string value = Console.ReadLine();

                //this section checks if the inputted values are either * or .
                //also the length of inputted values must equal to length of columns
                bool vaildValues = false;
                while (vaildValues == false)
                {
                    if (value.Length != columns)
                    {
                        value = Menu.MessageAndInput("incorrect length! please enter again for this row");
                    }
                    else
                    {
                        if (!AddValuesToSquare(s, value, columns,x))
                            value = Menu.MessageAndInput("incorrect values! please only input \"*\" or \".\"");
                        else
                        {
                            vaildValues = true;
                        }
                    }
                 }
              }
                return s;
          }

        //method to test if the rows and columns are successfully set by being retrieved from string input
        private static bool TestRowsAndColumnsInput(string str, out int rows, out int columns)
        {
            string num = null;
            int blankLocation;

            //a section to catch any exception message during transforming from string to int, and searching for blank space.
            try
            {
                blankLocation = str.IndexOf(" ");

                num = str.Substring(0, blankLocation);
                rows = int.Parse(num);

                num = str.Substring(blankLocation);
                columns = int.Parse(num);
            }
            catch
            {
                rows = 0;
                columns = 0;
                return false;
            }

            //chech if the number of rows or columns is from 1 to 99 
            if (rows >= 100 || rows <= 0 || columns >= 100 || columns <= 0) return false;
            return true;
        }

        //method to check the validation of inputted values and then give them to the square to create instance of Mines and the Unmineds.
        private static bool AddValuesToSquare(Squares s, string str, int columns, int row)
        {
            for (int y = 0; y < columns; y++)
            {
                if (str[y] != '.' && str[y] != '*') return false;
                s.Add(row, y, str[y]);
            }
            return true;
        }
    }


}
