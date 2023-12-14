using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2
    {
        public Day2()
        {
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
                    sum += fewestBlue * fewestGreen * fewestRed;

                }
                Console.WriteLine("Day 2, Part 2 Sum is: " + sum);
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
            Console.WriteLine("Day 2 Part 1 Sum is: " + sum);
        }
    }
}
