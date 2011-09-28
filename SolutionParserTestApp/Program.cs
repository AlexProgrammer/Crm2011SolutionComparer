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
            var document = XDocument.Load(File.OpenRead(@"C:\Users\Alex\Documents\Visual Studio 2010\Projects\CrmSolutionCompare\SolutionParserTestApp\bin\Debug\BaseDev_0_9_10_0\customizations.xml"));
            var entities = document.Element("ImportExportXml").Element("Entities").Elements("Entity");

            var result = from e in entities
                         where e.Element("EntityInfo") == null
                             select e.Element("Name").Value;
            new SolutionParseRunner().Run("BaseDev_0_9_10_0.zip");
        }
    }
}
