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
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            var solution = CrmSolution.Open(filePath);
            Console.WriteLine(solution.ToString());
            Console.WriteLine(solution.Publisher.ToString());
            Console.WriteLine(string.Format("Number of components: {0}", solution.Components.Count));
            Console.WriteLine(string.Format("Number of missing dependencies: {0}", solution.MissingDependencies.Count));
            Console.ReadKey();
        }
    }
}
