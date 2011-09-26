using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using Alex.Net.Crm.SolutionCompare.Parser.Objects;

namespace Alex.Net.Crm.SolutionCompare.Parser
{
    public partial class CrmSolution
    {
        protected static XNamespace xsiNameSpace = "http://www.w3.org/2001/XMLSchema-instance";

        protected static readonly object solutionComponentLock;

        protected static void ParseSolutionXml(CrmSolution solution, XDocument solutionDocument)
        {
            ParseSolutionRootElement(solution, solutionDocument);
            var manifestElement = solutionDocument.Element("ImportExportXml").Element("SolutionManifest");
            solution.UniqueName = manifestElement.Element("UniqueName").Value;
            solution.Name = ParseLocalizedLabelElement(manifestElement.Element("LocalizedNames"), solution.DefaultLanguageCode);
            // solution.Description ??
            solution.Version = manifestElement.Element("Version").Value;
            solution.IsManaged = manifestElement.Element("Managed").Value.Equals("1") ? true : false;
            solution.Publisher = ParsePublisher(manifestElement.Element("Publisher"), solution.DefaultLanguageCode);
            solution.Components = ParseRootComponents(manifestElement.Element("RootComponents"));
            solution.MissingDependencies = ParseMissingDependencies(manifestElement.Element("MissingDependencies"));
        }

        protected static void ParseSolutionRootElement(CrmSolution solution, XDocument solutionDocument)
        {
            int languageCode;
            solution.CrmVersion = solutionDocument.Element("ImportExportXml").Attribute("version").Value;
            solution.CrmMinimumVersion = solutionDocument.Element("ImportExportXml").Attribute("minimumversion").Value;
            solution.DefaultLanguageCode = int.TryParse(solutionDocument.Element("ImportExportXml")
                .Attribute("languagecode").Value, out languageCode) ? languageCode : -1; ;
            solution.GeneratedBy = solutionDocument.Element("ImportExportXml").Attribute("generatedBy").Value;
        }

        #region Publisher part parsing
        private static PublisherInfo ParsePublisher(XElement publisherElement, int defaultLanguageCode)
        {
            var publisherInfo = new PublisherInfo();
            publisherInfo.UniqueName = publisherElement.Element("UniqueName").Value;
            publisherInfo.Name = ParseLocalizedLabelElement(publisherElement.Element("LocalizedNames"), defaultLanguageCode);
            publisherInfo.Description = ParseLocalizedLabelElement(publisherElement.Element("Descriptions"), defaultLanguageCode);
            publisherInfo.EmailAddress = GetElementValueOrNull(publisherElement.Element("EMailAddress"));
            publisherInfo.SupportSite = GetElementValueOrNull(publisherElement.Element("SupportingWebsiteUrl"));
            publisherInfo.CustomizationPrefix = publisherElement.Element("CustomizationPrefix").Value;
            int customizationOptionValue;
            publisherInfo.CustomizationOptionValuePrefix = int.TryParse(publisherElement.Element("CustomizationOptionValuePrefix").Value,
                out customizationOptionValue) ? customizationOptionValue : 0;

            XElement addressesElement = publisherElement.Element("Addresses");
            publisherInfo.Address1 = GetPublisherAddress(GetAddress(addressesElement, "1"));
            publisherInfo.Address2 = GetPublisherAddress(GetAddress(addressesElement, "2"));

            return publisherInfo;

        }

        private static XElement GetAddress(XElement addresses, string addressNumber)
        {
            return (from a in addresses.Elements()
                    where a.Element("AddressNumber").Value.Equals(addressNumber)
                    select a).FirstOrDefault();
        }

        private static PublisherAddress GetPublisherAddress(XElement addressElement)
        {
            PublisherAddress publisherAddress = new PublisherAddress();

            int addressNumber;
            int addressTypeCode;
            int freightTermsCode;
            int importSequenceNumber;
            int shippingMethodCode;

            publisherAddress.AddressNumber = int.TryParse(addressElement.Element("AddressNumber").Value, out addressNumber) ? addressNumber : 0;
            publisherAddress.AddressTypeCode = int.TryParse(addressElement.Element("AddressTypeCode").Value, out addressTypeCode) ? addressTypeCode : 0;

            publisherAddress.City = GetElementValueOrNull(addressElement.Element("City"));
            publisherAddress.County = GetElementValueOrNull(addressElement.Element("County"));
            publisherAddress.Country = GetElementValueOrNull(addressElement.Element("Country"));
            publisherAddress.Fax = GetElementValueOrNull(addressElement.Element("Fax"));

            publisherAddress.FreightTermsCode = int.TryParse(GetElementValueOrNull(addressElement.Element("FreightTermsCode")), out freightTermsCode) ? freightTermsCode : 0;
            publisherAddress.ImportSequenceNumber = int.TryParse(GetElementValueOrNull(addressElement.Element("ImportSequenceNumber")), out importSequenceNumber) ? importSequenceNumber : 0;

            publisherAddress.Latitude = GetElementValueOrNull(addressElement.Element("Latitude"));
            publisherAddress.Line1 = GetElementValueOrNull(addressElement.Element("Line1"));
            publisherAddress.Line2 = GetElementValueOrNull(addressElement.Element("Line2"));
            publisherAddress.Line3 = GetElementValueOrNull(addressElement.Element("Line3"));
            publisherAddress.Longitude = GetElementValueOrNull(addressElement.Element("Longitude"));
            publisherAddress.Name = GetElementValueOrNull(addressElement.Element("Name"));
            publisherAddress.PostalCode = GetElementValueOrNull(addressElement.Element("PostalCode"));
            publisherAddress.PostOfficeBox = GetElementValueOrNull(addressElement.Element("PostOfficeBox"));
            publisherAddress.PrimaryContactName = GetElementValueOrNull(addressElement.Element("PrimaryContactName"));

            publisherAddress.ShippingMethodCode = int.TryParse(GetElementValueOrNull(addressElement.Element("ShippingMethodCode")), out shippingMethodCode) ? shippingMethodCode : 0;

            publisherAddress.StateOrProvince = GetElementValueOrNull(addressElement.Element("StateOrProvince"));
            publisherAddress.Telephone1 = GetElementValueOrNull(addressElement.Element("Telephone1"));
            publisherAddress.Telephone2 = GetElementValueOrNull(addressElement.Element("Telephone2"));
            publisherAddress.Telephone3 = GetElementValueOrNull(addressElement.Element("Telephone3"));
            publisherAddress.TimeZoneRuleVersionNumber = GetElementValueOrNull(addressElement.Element("TimeZoneRuleVersionNumber"));
            publisherAddress.UPSZone = GetElementValueOrNull(addressElement.Element("UPSZone"));
            publisherAddress.UTCOffset = GetElementValueOrNull(addressElement.Element("UTCOffset"));
            publisherAddress.UTCConversionTimeZoneCode = GetElementValueOrNull(addressElement.Element("UTCConversionTimeZoneCode"));

            return publisherAddress;
        }
        #endregion

        #region Component part parsing
        protected static List<RootComponent> ParseRootComponents(XElement rootComponentsElement)
        {
            List<RootComponent> rootComponents = new List<RootComponent>();
            foreach (var rootComponentElement in rootComponentsElement.Elements("RootComponent"))
            {
                var rootComponent = new RootComponent();
                {
                    var attributeTypeValue = int.Parse(rootComponentElement.Attribute("type").Value);
                    rootComponent.Type = Enum.IsDefined(typeof(ComponentType), attributeTypeValue) ? (ComponentType)attributeTypeValue : ComponentType.Undefined;
                }
                rootComponent.Id = rootComponentElement.Attribute("id") != null ? new Guid(rootComponentElement.Attribute("id").Value) : Guid.Empty;
                rootComponent.ParentId = rootComponentElement.Attribute("parentId") != null ? new Guid(rootComponentElement.Attribute("parentId").Value) : Guid.Empty;
                rootComponent.SchemaName = rootComponentElement.Attribute("schemaName") != null ? rootComponentElement.Attribute("schemaName").Value : null;
                rootComponents.Add(rootComponent);
            }
            return rootComponents;
        }
        #endregion

        #region Missing dependencies part parsing
        protected static List<Dependency> ParseMissingDependencies(XElement missingDependenciesElement)
        {
            List<Dependency> missingDependencies = new List<Dependency>();
            foreach (var dependencyElement in missingDependenciesElement.Elements("MissingDependency"))
            {
                var dependency = new Dependency();
                {
                    var requiredElement = dependencyElement.Element("Required");
                    var attributeTypeValue = int.Parse(requiredElement.Attribute("type").Value);

                    dependency.Required.Id = new Guid(requiredElement.Attribute("id").Value);
                    dependency.Required.Key = int.Parse(requiredElement.Attribute("key").Value);
                    dependency.Required.Type = Enum.IsDefined(typeof(ComponentType), attributeTypeValue) ? (ComponentType)attributeTypeValue : ComponentType.Undefined;

                    dependency.Required.DisplayName = requiredElement.Attribute("displayName").Value;
                    dependency.Required.SchemaName = requiredElement.Attribute("schemaName").Value;
                    dependency.Required.ParentDisplayName = requiredElement.Attribute("parentDisplayName").Value;
                    dependency.Required.ParentSchemaName = requiredElement.Attribute("parentSchemaName").Value;
                    dependency.Required.Solution = requiredElement.Attribute("solution").Value;
                }

                missingDependencies.Add(dependency);
            }
            return missingDependencies;
        }
        #endregion

        protected static string GetElementValueOrNull(XElement element)
        {
            return element.Attribute(xsiNameSpace + "nil") != null && element.Attribute(xsiNameSpace + "nil").Value.Equals("true") ? null : element.Value;
        }

        protected static Label ParseLocalizedLabelElement(XElement localizedElement, int defaultLanguageCode)
        {
            Label label = new Label(defaultLanguageCode);
            label.AddLocalizedLabels(
                from e in localizedElement.Elements()
                select new LocalizedLabel()
                {
                    Value = e.Attribute("description").Value,
                    LanguageCode = int.Parse(e.Attribute("languagecode").Value)
                });
            return label;
        }
    }
}
