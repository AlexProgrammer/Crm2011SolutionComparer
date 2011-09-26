using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Xml;
using Alex.Net.Crm.SolutionCompare.Parser.Objects;
using System.Xml.Linq;

namespace Alex.Net.Crm.SolutionCompare.Parser
{
    public partial class CrmSolution
    {
        public string CrmVersion { get; set; }
        public string CrmMinimumVersion { get; set; }
        public int DefaultLanguageCode { get; set; }
        public string GeneratedBy { get; set; }

        public bool IsManaged { get; private set; }
        public string Version { get; set; }
        public string UniqueName { get; set; }
        public Label Name { get; set; }

        public PublisherInfo Publisher { get; private set; }

        public List<RootComponent> Components { get; private set; }
        public List<Dependency> MissingDependencies { get; private set; }
        
        private const string customizationsXmlFilename = "customizations.xml";
        private const string contentTypesXmlFilename = "[Content_Types].xml";
        private const string solutionXmlFilename = "solution.xml";

        private const int bufferSize = 4096;

        public static CrmSolution Open(string crmSolutionFile)
        {
            using (Stream fileStream = File.OpenRead(crmSolutionFile))
            {
                return Parse(fileStream);
            }
        }

        public static CrmSolution Parse(Stream crmSolutionStream)
        {
            CrmSolution solution = new CrmSolution();
            ZipFile zipFile = new ZipFile(crmSolutionStream);
            var solutionXmlDocument = solution.GetXDocument(zipFile, solutionXmlFilename);
            var contentTypesXmlDocument = solution.GetXDocument(zipFile, contentTypesXmlFilename);
            var customizationsXmlDocument = solution.GetXDocument(zipFile, solutionXmlFilename);

            ParseSolutionXml(solution, solutionXmlDocument);

            return solution;
        }

        private XDocument GetXDocument(ZipFile zipFile, string xmlFileName)
        {
            var targetEntry = zipFile.GetEntry(xmlFileName);
            if (targetEntry != null)
            {
                XDocument resultDocument = null;
                //using (var memoryStream = new MemoryStream())
                //{
                    using (var zipStream = zipFile.GetInputStream(targetEntry))
                    {
                        resultDocument = XDocument.Load(zipStream);
                    }
                    
                //}
                return resultDocument;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ({1}) v {2}", this.Name.UserLocalizedLabel.Value, this.UniqueName, this.Version);
        }
    }
}
