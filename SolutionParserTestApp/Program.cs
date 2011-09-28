using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace SolutionParserTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new SolutionParseRunner().Run("BaseDev_0_9_10_0.zip");
        }
    }
}
