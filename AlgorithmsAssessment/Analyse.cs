using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAssessment
{
    public class Analyse
    {
        public List<int> ReadFile(string path)
        {
            // Opens the file
            using (var file = new StreamReader(path))
            {
                string line;
                var shares = new List<int>();

                // Reads each line of the file and appends to the list of shares
                while ((line = file.ReadLine()) != null)
                {
                    shares.Add(int.Parse(line));
                }

                return shares;
            }
        }
    }
}
