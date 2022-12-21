using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssessment
{
    public class Search
    {
        // Find and return the location(s) of the closest number if couldnt
        // find original searched number
        public void FindClosest(List<int> list, int i, int value)
        {
            Console.WriteLine($"Value not found, searching for closest" +
                $" number ({value}).\n");

            // Moves to the leftmost instance of this value
            while (true)
            {
                if (list[i - 1] == value)
                {
                    i--;
                }
                else
                {
                    break;
                }
            }

            LinearSearch(list, value, i);
        }

        // Linear search algorithm
        public List<int> LinearSearch(List<int> list, int value, int start_index = 0)
        {
            var positions = new List<int>();

            // Checks that value is between the values at each end of the list
            if (0 <= list.Count - 1 && value >= 0 && value <= list.Count - 1)
            {
                // Iterates through the list and grabs the index of values that
                // match the search value
                for (int i = start_index; i < list.Count; i++)
                {
                    // If the loop has passed the value then return, else continue
                    if (list[i] > value)
                    {
                        // If the value is not located, find closest number
                        if (positions.Count == 0)
                        {
                            if (list[i] - value < value - list[i - 1])
                            {
                                FindClosest(list, i, list[i]);
                            }
                            else
                            {
                                FindClosest(list, i - 1, list[i - 1]);
                            }
                        }
                    }
                    else if (list[i] == value)
                    {
                        positions.Add(i);
                    }
                }
            }

            return positions;
        }

        // Jump search algorithm
        public List<int> JumpSearch(List<int> list, int value)
        {
            int i;
            var positions = new List<int>();
            int jump_size = Convert.ToInt32(Math.Round(Math.Sqrt(list.Count), 0));

            // Checks that value is between the values at each end of the list
            if (0 <= list.Count - 1 && value >= 0 && value <= list.Count - 1)
            {
                // Iterates through the list in jumps of the root of the list size
                // until the item is greater than the chosen value, then goes back
                // and performs a regular linear search to find the value positions
                for (i = 0; i < list.Count; i += jump_size)
                {
                    if (list[i] > value)
                    {
                        i -= jump_size;
                        break;
                    }
                }

                // Perform a linear search on the remaining few values
                positions = LinearSearch(list, value, i);
            }

            return positions;
        }

        // Binary search to find the lower bound of the values
        public int BinarySearchLower(List<int> list, int value, int left, int right)
        {
            int middle = left + (right - left) / 2;

            if (left > right)
            {
                return left;
            }

            // Compares the middle element to the value to be
            // found if lower than or equal to move to the left
            if (list[middle] >= value)
            {
                return BinarySearchLower(list, value, left, middle - 1);
            }
            else
            {
                return BinarySearchLower(list, value, middle + 1, right);
            }
        }

        // Binary search algorithm
        public List<int> BinarySearch(List<int> list, int value)
        {
            int left = 0;
            int right = list.Count - 1;
            var positions = new List<int>();

            // Checks that value is between the values at each end of the list
            if (0 <= list.Count - 1 && value >= 0 && value <= list.Count - 1)
            {
                // Performs a binary search to find the lowest index
                // that the value can be found at
                int lower = BinarySearchLower(list, value, left, right);

                // Performs a linear search starting with the lower bound
                positions = LinearSearch(list, value, lower);
            }

            return positions;
        }

        // Interpolation search algorithm
        public List<int> InterpolationSearch(List<int> list, int value, int lower, int upper)
        {
            int pointer;
            var positions = new List<int>();

            // Checks that value is between the values at each end of the list
            if (0 <= list.Count - 1 && value >= 0 && value <= list.Count - 1)
            {
                // Equation to look for a likely position of the value 
                // assuming the list is uniform
                pointer = lower + (((upper - lower) / (list[upper] - list[lower]))
                    * (value - list[lower]));

                if (list[pointer] == value)
                {
                    // Keeps moving the pointer left until it reaches the
                    // leftmost element equal to the value being searched for
                    while (list[pointer - 1] == value)
                    {
                        pointer--;
                    }

                    positions = LinearSearch(list, value, pointer);
                }

                // If value less than value at pointer,
                // check elements to the left
                if (list[pointer] > value)
                {
                    return InterpolationSearch(list, value, lower, pointer - 1);
                }

                // If value greater than value at pointer,
                // check elements to the right
                if (list[pointer] < value)
                {
                    return InterpolationSearch(list, value, pointer + 1, upper);
                }
            }

            return positions;
        }
    }
}
