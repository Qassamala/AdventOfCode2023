using System.Net.Http.Headers;
using System.Text.RegularExpressions;

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
            int counter = 1;

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

        private static string extractInt(String number)
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