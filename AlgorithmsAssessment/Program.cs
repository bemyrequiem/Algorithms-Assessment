using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssessment
{
    class Program
    {
        static void Main()
        {
            string option;
            string path;
            int n;
            int to_find;
            List<int> to_search;
            var shares = new List<List<int>>();

            // Defining classes
            var analyse = new Analyse();
            var sort = new Sort();
            var search = new Search();

            // Print unsorted lists to the console
            void PrintShares(List<List<int>> list)
            {
                foreach (List<int> l in list)
                {
                    string line = "";
                    for (int i = 0; i < l.Count; i += n)
                    {
                        line += l[i].ToString();
                        line += ", ";
                    }
                    line = line.Remove(line.Length - 2, 2);
                    Console.WriteLine($"{line}\n");
                }
            }

            // Converts a list to a string representation
            string ListToString(List<int> list)
            {
                string str = "[";

                foreach (int i in list)
                {
                    str += i.ToString();
                    str += ", ";
                }

                str = str.Remove(str.Length - 2, 2);
                str += "]";
                return str;
            }

            // Prints positions of number after search results
            void PrintPositions(List<int> list)
            {
                string positions;

                if (list.Count > 1)
                {
                    positions = ListToString(list);
                    Console.WriteLine($"\nNumber found at positions {positions}." +
                        $" (Starting at index 0)");
                }
                else if (list.Count > 0)
                {
                    positions = list[0].ToString();
                    Console.WriteLine($"\nNumber found at position {positions}." +
                        $" (Starting at index 0)");
                }
                else
                {
                    Console.WriteLine("\nNumber was not found.");
                }
            }

            // Ask user for path to the shares
            while (true)
            {
                Console.WriteLine("What is the path of the directory" +
                    " containing the shares files?\n\nPlease make sure" +
                    " the files are named in the format 'Share_1_256'.");
                path = Console.ReadLine();

                // Check if path exists
                if (Directory.Exists(path))
                {
                    if (!path.EndsWith("\\"))
                    {
                        path += "\\";
                    }
                    break;
                }
                else
                {
                    // If path does not exist, make the user reinput the path
                    Console.WriteLine("\nPath does not exist, please try again.");
                }
            }


            // Main while loop to get the users choice of shares to read
            while (true)
            {
                // Asking the user which share type to read
                Console.WriteLine("\nWhich file size would you like" +
                    " to read, 256 or 2048?");
                option = Console.ReadLine();

                // Checks if the user inputted a valid size
                if (option == "256" || option == "2048")
                {
                    bool cont = true;
                    int i = 1;

                    // Sets size variable based on which size is selected
                    if (option == "256")
                    {
                        n = 10;
                    }
                    else
                    {
                        n = 50;
                    }
                    
                    while (cont)
                    {
                        // Finds all shares files of the specified size until
                        // no more of that length remain
                        try
                        {
                            // Calls the ReadFile function in the Analyse class with
                            // the path and file name, incrementing the number on the
                            // file until there are no more found
                            string full_path = $"{path}Share_{i}_{option}.txt";
                            List<int> list = analyse.ReadFile(full_path);
                            shares.Add(list);
                            i++;
                        }
                        catch (FileNotFoundException)
                        {
                            cont = false;
                        }
                    }

                    //PrintShares(shares, 1);
                    break;
                }
                else
                {
                    Console.WriteLine("\nThat is not a valid file length," +
                        " please try again.");
                }
            }

            void Main()
            {
                // Performs the chosen sorting algorithm on the shares
                while (true)
                {
                    bool ascending = true;

                    Console.WriteLine("\nWhich sorting algorithm would you like to" +
                        " use to sort the shares?\n1. Bubble sort\n2. Merge sort");
                    option = Console.ReadLine();

                    // Asks if the user would like ascending or descending (default asc)
                    Console.WriteLine("\n Would you like to sort in ascending or" +
                        " descending order? (a/d)");
                    string option2 = Console.ReadLine();

                    if (option2 == "d")
                    {
                        ascending = false;
                    }

                    // Checks if the user chose a valid option
                    if (option == "1")
                    {
                        for (int i = 0; i < shares.Count; i++)
                        {
                            shares[i] = sort.BubbleSort(shares[i]);

                            if (!ascending)
                            {
                                shares[i].Reverse();
                            }

                            sort.PrintSteps();
                        }

                        PrintShares(shares);
                        break;
                    }
                    else if (option == "2")
                    {
                        for (int i = 0; i < shares.Count; i++)
                        {
                            sort.MergeSort(shares[i], 0, shares[i].Count - 1);

                            if (!ascending)
                            {
                                shares[i].Reverse();
                            }

                            sort.PrintSteps();
                        }

                        PrintShares(shares);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nThat is not a valid option.");
                    }
                }


                // Ask the user which shares are to be searched
                while (true)
                {
                    Console.WriteLine("Which shares file would you like to search?" +
                        "\n(enter the number in the middle of the file name)");
                    option = Console.ReadLine();

                    try
                    {
                        to_search = shares[int.Parse(option) - 1];
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("\nThat is not a valid option.");
                    }
                }

                // Ask user what number is to be searched for
                while (true)
                {
                    Console.WriteLine("\nWhat is the number you would like to search for?");
                    option = Console.ReadLine();

                    try
                    {
                        to_find = int.Parse(option);
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("\nThat is not a valid integer.");
                    }
                }

                // Performs the chosen search algorithm on the chosen share to
                // search for the value the user inputs
                while (true)
                {
                    Console.WriteLine("\nWhich search algorithm would you like to" +
                        " use to search the shares?\n1. Linear search\n2. Jump" +
                        " search\n3. Binary search\n4. Interpolation search");
                    option = Console.ReadLine();

                    // Checks if the user chose a valid option
                    if (option == "1")
                    {
                        List<int> indicies = search.LinearSearch(to_search, to_find);
                        PrintPositions(indicies);
                        break;
                    }
                    else if (option == "2")
                    {
                        List<int> indicies = search.JumpSearch(to_search, to_find);
                        PrintPositions(indicies);
                        break;
                    }
                    else if (option == "3")
                    {
                        List<int> indicies = search.BinarySearch(to_search, to_find);
                        PrintPositions(indicies);
                        break;
                    }
                    else if (option == "4")
                    {
                        List<int> indicies = search.InterpolationSearch(to_search,
                            to_find, 0, to_search.Count() - 1);
                        PrintPositions(indicies);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid option.");
                    }
                }
            }

            Main();

            // Extra task 6

            // 256 files
            n = 10;
            var temp = new List<int>();
            shares = new List<List<int>>();
            Console.WriteLine("\n\n\nTASK 6 256 FILES");

            // Add both contents of the 256 files to a temporary list
            foreach (int i in analyse.ReadFile($"{path}Share_1_256.txt"))
            {
                temp.Add(i);
            }

            foreach (int i in analyse.ReadFile($"{path}Share_3_256.txt"))
            {
                temp.Add(i);
            }

            // Add temporary list to the shares list
            shares.Add(temp);

            Main();

            // 2048 files
            n = 50;
            temp = new List<int>();
            shares = new List<List<int>>();
            Console.WriteLine("\n\n\nTASK 6 2048 FILES");

            // Add both contents of the 2048 files to a temporary list
            foreach (int i in analyse.ReadFile($"{path}Share_1_2048.txt"))
            {
                temp.Add(i);
            }

            foreach (int i in analyse.ReadFile($"{path}Share_3_2048.txt"))
            {
                temp.Add(i);
            }

            // Add temporary list to the shares list
            shares.Add(temp);

            Main();
        }
    }
}
