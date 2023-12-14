using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3
    {
        public Day3()
        {
            string filelocDay3 = "Day3Input.txt";
            //Day3Part1Deprecated(filelocDay3);

            //Day3Part1(filelocDay3);
        }

        private static void Day3Part1(string filelocDay3)
        {
            string contents = File.ReadAllText(filelocDay3);
            var result = PartOne(contents);
            Console.WriteLine(result);
        }

        public static object PartOne(string input)
        {
            var rows = input.Split("\n");
            var symbols = Parse(rows, new Regex(@"[^.0-9]"));
            var nums = Parse(rows, new Regex(@"\d+"));

            return (
                from n in nums
                where symbols.Any(s => Adjacent(s, n))
                select n.Int
            ).Sum();
        }
        record Part(string Text, int Irow, int Icol)
        {
            public int Int => int.Parse(Text);
        }



        static bool Adjacent(Part p1, Part p2) =>
            Math.Abs(p2.Irow - p1.Irow) <= 1 &&
            p1.Icol <= p2.Icol + p2.Text.Length &&
            p2.Icol <= p1.Icol + p1.Text.Length;

        static Part[] Parse(string[] rows, Regex rx) => (
            from irow in Enumerable.Range(0, rows.Length)
            from match in rx.Matches(rows[irow])
            select new Part(match.Value, irow, match.Index)
            ).ToArray();

        private static void Day3Part1Deprecated(string filelocDay3)
        {
            string line;
            int rowNumber = 0;
            int columnNumber = 0;

            char[,] matrix = new char[140, 140];

            using (StreamReader read = new StreamReader(filelocDay3))
            {
                while ((line = read.ReadLine()) != null)
                {
                    foreach (char c in line)
                    {
                        matrix[rowNumber, columnNumber++] = c;
                    }
                    columnNumber = 0;
                    rowNumber++;
                }
            }

            int sum = 0;
            string number = "";
            int numberLength = 0;
            bool valid = false;

            for (int row = 0; row < matrix.GetLength(0); ++row)
            {
                for (int col = 0; col < matrix.GetLength(1); ++col)
                {
                    if (Char.IsDigit(matrix[row, col]))
                    {
                        number += matrix[row, col];
                        numberLength++;
                    }
                    else if (numberLength > 0)
                    {
                        bool leftmost = true, rightmost = true, above = true, below = true;
                        if (row == 0)
                        {
                            above = false;
                        }
                        else if (row == 139)
                        {
                            below = false;
                        }

                        if (col - 1 - numberLength < 0)
                        {
                            leftmost = false;
                        }
                        else if (col == 139)
                        {
                            rightmost = false;
                        }

                        if (number.Equals("777"))
                        {
                            Console.WriteLine(number);
                        }

                        valid = checkIfValidnr(matrix, row, col, numberLength, leftmost, rightmost, above, below);

                        if (valid)
                        {
                            sum += ushort.Parse(number);
                            Console.WriteLine("Adding: " + number + " and Sum is now: " + sum);
                            number = "";
                            numberLength = 0;
                            valid = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid number: " + number + " and Sum is still: " + sum);
                            number = "";
                            numberLength = 0;
                        }
                    }


                }
            }
            Console.WriteLine("Adding: " + number + " and Sum is now: " + sum);

        }
        private static bool checkIfValidnr(char[,] matrix, int currentRow, int currentColumn, int numberLength,
            bool leftmostAvailable, bool rightmostAvailable, bool aboveAvailable, bool belowAvailable)
        {
            if (currentColumn == 0 && currentRow > 0)
            {
                currentColumn = 139;
                currentRow = currentRow - 1;
                numberLength = numberLength - 1;
                leftmostAvailable = true;
            }




            char c = matrix[currentRow, currentColumn];

            Console.WriteLine("Row: " + currentRow + " Column: " + currentColumn);


            // check if current pos is a symbol
            if (!char.IsLetterOrDigit(c) && c != '.')
            {
                return true;
            }
            else if (leftmostAvailable && (!char.IsLetterOrDigit(matrix[currentRow, currentColumn - 1 - numberLength]) && matrix[currentRow, currentColumn - 1 - numberLength] != '.'))    //check leftmost
            {
                return true;
            }
            else if (leftmostAvailable && aboveAvailable && (!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 1 - numberLength]) && matrix[currentRow - 1, currentColumn - 1 - numberLength] != '.'))    // check diagonally left up
            {
                return true;
            }
            else if (leftmostAvailable && belowAvailable && (!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1 - numberLength]) && matrix[currentRow + 1, currentColumn - 1 - numberLength] != '.')) // check diagonally left below
            {
                return true;
            }
            else if (rightmostAvailable && aboveAvailable && (!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn]) && matrix[currentRow - 1, currentColumn] != '.')) // check diagonally above right
            {
                return true;
            }
            else if (rightmostAvailable && belowAvailable && (!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn]) && matrix[currentRow + 1, currentColumn] != '.')) // check diagonally below right
            {
                return true;
            }
            else if (belowAvailable && (numberLength == 2 && ((!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1]) && matrix[currentRow + 1, currentColumn - 1] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 2]) && matrix[currentRow + 1, currentColumn - 2] != '.'))) //check first and last digit below
            {
                return true;
            }
            else if (aboveAvailable && (numberLength == 2 && ((!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 2]) && matrix[currentRow - 1, currentColumn - 2] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 1]) && matrix[currentRow - 1, currentColumn - 1] != '.'))) //check first and last digit above
            {
                return true;
            }
            else if ((leftmostAvailable && aboveAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 2]) && matrix[currentRow - 1, currentColumn - 2] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 1]) && matrix[currentRow - 1, currentColumn - 1] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 3]) && matrix[currentRow - 1, currentColumn - 3] != '.')) //check first, middle and last digit above
            {
                return true;
            }
            else if ((leftmostAvailable && belowAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 2]) && matrix[currentRow + 1, currentColumn - 2] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1]) && matrix[currentRow + 1, currentColumn - 1] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 3]) && matrix[currentRow + 1, currentColumn - 3] != '.')) //check first, middle and last digit below
            {
                return true;
            }
            else if ((!leftmostAvailable && belowAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 2]) && matrix[currentRow + 1, currentColumn - 2] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1]) && matrix[currentRow + 1, currentColumn - 1] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 3]) && matrix[currentRow + 1, currentColumn - 3] != '.')) //check first, middle and last digit below
            {
                return true;
            }
            else if ((!leftmostAvailable && aboveAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 2]) && matrix[currentRow + 1, currentColumn - 2] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1]) && matrix[currentRow + 1, currentColumn - 1] != '.') ||
                !char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 3]) && matrix[currentRow + 1, currentColumn - 3] != '.')) //check first, middle and last digit above
            {
                return true;
            }
            else if (!leftmostAvailable && rightmostAvailable && aboveAvailable && (!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn]) && matrix[currentRow - 1, currentColumn] != '.')) // check diagonally above right
            {
                return true;
            }
            else if (!leftmostAvailable && rightmostAvailable && belowAvailable && (!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn]) && matrix[currentRow + 1, currentColumn] != '.')) // check diagonally below right
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
