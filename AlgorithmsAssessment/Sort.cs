using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssessment
{
    public class Sort
    {
        int steps;

        public void ResetSteps()
        {
            steps = 0;
        }

        public void PrintSteps()
        {
            Console.WriteLine($"\nPerformed in {steps} steps.");
        }

        // Bubble sorting algorithm
        public List<int> BubbleSort(List<int> list)
        {
            List<int> result = list;
            int n = result.Count;
            steps = 0;

            for (int a = 0; a < n - 1; a++)
            {
                for (int b = 0; b < n - a - 1; b++)
                {
                    if (result[b] > result[b + 1])
                    {
                        // Swaps the two values
                        int temp = result[b];
                        result[b] = result[b + 1];
                        result[b + 1] = temp;
                        steps++;
                    }
                }
            }

            return result;
        }

        // Merges two halves of a list 
        public void Merge(List<int> list, int left, int right, int middle)
        {
            // Find sizes of the two sections being merged
            int a = middle - left + 1;
            int b = right - middle;

            // Temporary lists
            int[] l = new int[a];
            int[] r = new int[b];
            int i, j;

            // Adds the sections to the temporary lists
            for (i = 0; i < a; ++i)
            {
                l[i] = list[left + i];
            }
            for (j = 0; j < b; ++j)
            {
                r[j] = list[middle + j + 1];
            }

            i = 0;
            j = 0;
            int k = left;

            // Comparing items in the temporary lists and copy them to main list
            while (i < a && j < b)
            {
                if (l[i] <= r[j])
                {
                    list[k] = l[i];
                    i++;
                }
                else
                {
                    list[k] = r[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            while (i < a)
            {
                list[k] = l[i];
                i++;
                k++;
            }
        }

        // Merge sorting algorithm
        public void MergeSort(List<int> list, int left, int right)
        {
            // Only runs if the list had more than one element
            if (left < right)
            {
                // Gets the middle index of the section being looked at
                int middle = left + (right - left) / 2;

                // Performs merge sort on both halves of the current section
                MergeSort(list, left, middle);
                MergeSort(list, middle + 1, right);

                // Merges the two sections
                Merge(list, left, right, middle);
                steps++;
            }
        }
    }
}
