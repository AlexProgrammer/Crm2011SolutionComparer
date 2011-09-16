using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alex.Net.Crm.SolutionCompare.Parser;

namespace SolutionParserTestApp
{
    public class SolutionParseRunner
    {
        public void Run(string filePath)
        {
            var solution = CrmSolution.Open(filePath);
            Console.ReadKey();
        }
    }
}
