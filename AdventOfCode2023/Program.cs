using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileloc = "InputDay1Part1.txt";
            Day1Part1(fileloc);
            Day1Part2(fileloc);

            string filelocDay2 = "Day2Input.txt";
            Day2Part1(filelocDay2);
            Day2Part2(filelocDay2);

            string filelocDay3 = "Day3Input.txt";
            //Day3Part1Deprecated(filelocDay3);

            //Day3Part1(filelocDay3);

            string filelocDay4 = "Day4Input.txt";
            Day4Part1(filelocDay4);
            //Day4Part2(filelocDay4);


        }

        private static void Day4Part1(string filelocDay4)
        {
            double points = 0;
            string line;

            // For Part2
            Dictionary<int, int> instances = new Dictionary<int, int>();
            int cardNumber = 0;
            instances.Add(1, 1);
            int copiesWonUntilIncluding = 0;

            using (StreamReader read = new StreamReader(filelocDay4))
            {
                while ((line = read.ReadLine()) != null)
                {
                    cardNumber++;   //Part 2
                    double matchesFound = 0;
                    var card  = line.Split('|');
                    var chosenNumbers = card[1].Split(' ')
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray();

                    var winningNumbers = card[0].Split(' ').Skip(2)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray();

                    foreach (var number in winningNumbers)
                    {

                        foreach (var item in chosenNumbers)
                        {
                            if (item.Equals(number))
                            {
                                matchesFound++;
                                break;
                            }
                        }
                        
                    }

                    // For Part 2
                    copiesWonUntilIncluding = cardNumber + (int)matchesFound;
                    for (int i = copiesWonUntilIncluding; i > cardNumber; i--)
                    {
                        if (!instances.ContainsKey(i))
                        {
                            instances.Add(i, 1);
                        }
                        else
                        {
                            instances.TryGetValue(i, out var currentCount);
                            instances[i] = currentCount + 1;
                        }
                    }

                    // For part 1
                    if(matchesFound > 0)
                    {
                        matchesFound -= 1;

                        points += Math.Pow(2, matchesFound);
                    }

                    
                }
                Console.WriteLine("Day4Part1 Sum is: " + points);

                //Part 2
                Console.WriteLine("Day4Part2 Sum is: " + instances.Sum(x => x.Value));
                
            }

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
                    else if(numberLength > 0)
                    {
                        bool leftmost = true, rightmost = true, above = true, below = true;
                        if (row == 0)
                        {
                            above = false;
                        }
                        else if(row == 139)
                        {
                            below = false;
                        }

                        if (col-1-numberLength < 0)
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
                        } else
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
            if(currentColumn == 0 && currentRow > 0)
            {
                currentColumn = 139;
                currentRow = currentRow - 1;
                numberLength = numberLength - 1;
                leftmostAvailable= true;
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
                } else if (leftmostAvailable && belowAvailable && (!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 1 - numberLength]) && matrix[currentRow + 1, currentColumn - 1 - numberLength] != '.')) // check diagonally left below
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
                } else if (aboveAvailable && (numberLength == 2 && ((!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 2]) && matrix[currentRow - 1, currentColumn - 2] != '.') ||
                    !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 1]) && matrix[currentRow - 1, currentColumn - 1] != '.'))) //check first and last digit above
                {
                    return true;
                }
                else if ((leftmostAvailable && aboveAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 2]) && matrix[currentRow - 1, currentColumn - 2] != '.') ||
                    !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 1]) && matrix[currentRow - 1, currentColumn - 1] != '.') ||
                    !char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn - 3]) && matrix[currentRow - 1, currentColumn - 3] != '.')) //check first, middle and last digit above
                {
                    return true;
                } else if ((leftmostAvailable && belowAvailable) && (numberLength == 3 && ((!char.IsLetterOrDigit(matrix[currentRow + 1, currentColumn - 2]) && matrix[currentRow + 1, currentColumn - 2] != '.') ||
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
                }else if (!leftmostAvailable && rightmostAvailable && aboveAvailable && (!char.IsLetterOrDigit(matrix[currentRow - 1, currentColumn]) && matrix[currentRow - 1, currentColumn] != '.')) // check diagonally above right
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

      

        private static string checkIfDigit(string number)
        {
            switch (number)
            {
                case "one": return "1";
                case "two": return "2";
                case "three": return "3";
                case "four": return "4";
                case "five": return "5";
                case "six": return "6";
                case "seven": return "7";
                case "eight": return "8";
                case "nine": return "9";
                default:
                    return number;
            }

        }

        private static void Day2Part2(string filelocDay2)
        {
            int sum = 0;
            int red = 12, green = 13, blue = 14;
            int gameCounter = 0;
            


            using (StreamReader read = new StreamReader(filelocDay2))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    int fewestRed = 0, fewestGreen = 0, fewestBlue = 0;
                    gameCounter++;
                    var subsets = line.Split(':', ';');


                    for (int i = 1; i < subsets.Length; i++)
                    {
                        int foundRed = 0, foundGreen = 0, foundBlue = 0;
                        //Console.WriteLine(subsets[i]);
                        var splits = subsets[i].Split(' ', ',');
                        for (int j = 2; j < splits.Length; j++)
                        {
                            //Console.WriteLine(splits[j]);

                            if (splits[j].Equals("blue"))
                            {
                                foundBlue += ushort.Parse(splits[j - 1]);
                                if (foundBlue > fewestBlue)
                                {
                                    fewestBlue = foundBlue;
                                }
                            }
                            else if (splits[j].Equals("red"))
                            {
                                foundRed += ushort.Parse(splits[j - 1]);
                                if (foundRed > fewestRed)
                                {
                                    fewestRed = foundRed;
                                }
                            }
                            else if (splits[j].Equals("green"))
                            {
                                foundGreen += ushort.Parse(splits[j - 1]);
                                if (foundGreen > fewestGreen)
                                {
                                    fewestGreen = foundGreen;
                                }
                            }
                        }

                    }
                        //Console.WriteLine("Power: " + (fewestBlue * fewestGreen * fewestRed));
                        sum += fewestBlue*fewestGreen*fewestRed;

                }
                Console.WriteLine("Sum is: " + sum);
            }
        }

        private static void Day2Part1(string filelocDay2)
        {
            int sum = 0;
            int red = 12, green = 13, blue = 14;
            int gameCounter = 0;
            

            using (StreamReader read = new StreamReader(filelocDay2))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    bool impossible = false;
                    gameCounter++;
                    var subsets = line.Split(':', ';');

                    for (int i = 1; i < subsets.Length; i++)
                    {
                        int foundRed = 0, foundGreen = 0, foundBlue = 0;
                        //Console.WriteLine(subsets[i]);
                        var splits = subsets[i].Split(' ', ',');
                        for (int j = 2; j < splits.Length; j++)
                        {
                            //Console.WriteLine(splits[j]);

                            if (splits[j].Equals("blue"))
                            {
                                foundBlue += ushort.Parse(splits[j - 1]);
                            }
                            else if (splits[j].Equals("red"))
                            {
                                foundRed += ushort.Parse(splits[j - 1]);
                            }
                            else if (splits[j].Equals("green"))
                            {
                                foundGreen += ushort.Parse(splits[j - 1]);
                            }

                            if (foundBlue > blue || foundRed > red || foundGreen > green)
                            {
                                //Console.WriteLine("breaking inner");
                                impossible = true;
                                break;
                                
                            }
                        }
                        if (impossible)
                        {
                            //Console.WriteLine("breaking outer");
                            break;

                        }
                    }
                    if (!impossible)
                    {
                        sum += gameCounter;
                    }

                    
                }
            }
            Console.WriteLine("Sum is: " + sum);
        }

        private static void extractColorNumbers(string[] splits)
        {
            
        }


        private static void Day1Part1(string fileloc)
        {
            int sum = 0, calibration = 0;
            string combined = null;

            using (StreamReader read = new StreamReader(fileloc))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var numbers = Regex.Matches(line, @"\d");
                    combined = numbers.First().Value + numbers.Last().Value;
                    calibration = ushort.Parse(combined);
                    sum += calibration;
                }
            }

            Console.WriteLine("Part1 sum is: " + sum);
        }

        private static void Day1Part2(string fileloc)
        {
            string regex = @"\d|one|two|three|four|five|six|seven|eight|nine";
            int sum = 0, calibration = 0;
            //int counter = 1;

            using (StreamReader read = new StreamReader(fileloc))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var first = Regex.Match(line, regex);
                    var last = Regex.Match(line, regex, RegexOptions.RightToLeft);

                    calibration = ushort.Parse(extractInt(first.Value) + extractInt(last.Value));
                    sum += calibration;
                }
            }

            Console.WriteLine("Part2 sum is: " + sum);
        }

        private static string extractInt(string number)
        {
            switch (number)
            {
                case "one": return "1";
                case "two": return "2";
                case "three": return "3";
                case "four": return "4";
                case "five": return "5";
                case "six": return "6";
                case "seven": return "7";
                case "eight": return "8";
                case "nine": return "9"; 
                default:
                    return number;
            }
            
        }
    }
}