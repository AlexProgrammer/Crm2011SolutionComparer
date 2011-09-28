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

        protected static readonly object solutionComponentLock;

        protected static void ParseSolutionXml(CrmSolution solution, XDocument solutionDocument)
        {
            ParseSolutionRootElement(solution, solutionDocument);
            var manifestElement = solutionDocument.Element("ImportExportXml").Element("SolutionManifest");
            solution.UniqueName = manifestElement.Element("UniqueName").Value;
            solution.Name = Util.ParseLocalizedLabelElement(manifestElement.Element("LocalizedNames"), solution.DefaultLanguageCode);
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
            publisherInfo.Name = Util.ParseLocalizedLabelElement(publisherElement.Element("LocalizedNames"), defaultLanguageCode);
            publisherInfo.Description = Util.ParseLocalizedLabelElement(publisherElement.Element("Descriptions"), defaultLanguageCode);
            publisherInfo.EmailAddress = Util.GetElementValueOrNull(publisherElement.Element("EMailAddress"));
            publisherInfo.SupportSite = Util.GetElementValueOrNull(publisherElement.Element("SupportingWebsiteUrl"));
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

            publisherAddress.City = Util.GetElementValueOrNull(addressElement.Element("City"));
            publisherAddress.County = Util.GetElementValueOrNull(addressElement.Element("County"));
            publisherAddress.Country = Util.GetElementValueOrNull(addressElement.Element("Country"));
            publisherAddress.Fax = Util.GetElementValueOrNull(addressElement.Element("Fax"));

            publisherAddress.FreightTermsCode = int.TryParse(Util.GetElementValueOrNull(addressElement.Element("FreightTermsCode")), out freightTermsCode) ? freightTermsCode : 0;
            publisherAddress.ImportSequenceNumber = int.TryParse(Util.GetElementValueOrNull(addressElement.Element("ImportSequenceNumber")), out importSequenceNumber) ? importSequenceNumber : 0;

            publisherAddress.Latitude = Util.GetElementValueOrNull(addressElement.Element("Latitude"));
            publisherAddress.Line1 = Util.GetElementValueOrNull(addressElement.Element("Line1"));
            publisherAddress.Line2 = Util.GetElementValueOrNull(addressElement.Element("Line2"));
            publisherAddress.Line3 = Util.GetElementValueOrNull(addressElement.Element("Line3"));
            publisherAddress.Longitude = Util.GetElementValueOrNull(addressElement.Element("Longitude"));
            publisherAddress.Name = Util.GetElementValueOrNull(addressElement.Element("Name"));
            publisherAddress.PostalCode = Util.GetElementValueOrNull(addressElement.Element("PostalCode"));
            publisherAddress.PostOfficeBox = Util.GetElementValueOrNull(addressElement.Element("PostOfficeBox"));
            publisherAddress.PrimaryContactName = Util.GetElementValueOrNull(addressElement.Element("PrimaryContactName"));

            publisherAddress.ShippingMethodCode = int.TryParse(Util.GetElementValueOrNull(addressElement.Element("ShippingMethodCode")), out shippingMethodCode) ? shippingMethodCode : 0;

            publisherAddress.StateOrProvince = Util.GetElementValueOrNull(addressElement.Element("StateOrProvince"));
            publisherAddress.Telephone1 = Util.GetElementValueOrNull(addressElement.Element("Telephone1"));
            publisherAddress.Telephone2 = Util.GetElementValueOrNull(addressElement.Element("Telephone2"));
            publisherAddress.Telephone3 = Util.GetElementValueOrNull(addressElement.Element("Telephone3"));
            publisherAddress.TimeZoneRuleVersionNumber = Util.GetElementValueOrNull(addressElement.Element("TimeZoneRuleVersionNumber"));
            publisherAddress.UPSZone = Util.GetElementValueOrNull(addressElement.Element("UPSZone"));
            publisherAddress.UTCOffset = Util.GetElementValueOrNull(addressElement.Element("UTCOffset"));
            publisherAddress.UTCConversionTimeZoneCode = Util.GetElementValueOrNull(addressElement.Element("UTCConversionTimeZoneCode"));

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
                    var requiredAttributeTypeValue = int.Parse(requiredElement.Attribute("type").Value);
                    var dependentElement = dependencyElement.Element("Dependent");
                    var dependentAttributeTypeValue = int.Parse(dependentElement.Attribute("type").Value);

                    dependency.Required.Id = new Guid(requiredElement.Attribute("id").Value);
                    dependency.Required.Key = int.Parse(requiredElement.Attribute("key").Value);
                    dependency.Required.Type = Enum.IsDefined(typeof(ComponentType), requiredAttributeTypeValue) ? 
                        (ComponentType)requiredAttributeTypeValue : ComponentType.Undefined;
                    dependency.Required.DisplayName = requiredElement.Attribute("displayName").Value;
                    dependency.Required.SchemaName = requiredElement.Attribute("schemaName").Value;
                    dependency.Required.ParentDisplayName = requiredElement.Attribute("parentDisplayName").Value;
                    dependency.Required.ParentSchemaName = requiredElement.Attribute("parentSchemaName").Value;
                    dependency.Required.Solution = requiredElement.Attribute("solution").Value;

                    dependency.Dependent.Id = new Guid(dependentElement.Attribute("id").Value);
                    dependency.Dependent.Key = int.Parse(dependentElement.Attribute("key").Value);
                    dependency.Dependent.Type = Enum.IsDefined(typeof(ComponentType), dependentAttributeTypeValue) ? 
                        (ComponentType)dependentAttributeTypeValue : ComponentType.Undefined;
                    dependency.Dependent.ParentDisplayName = dependentElement.Attribute("parentDisplayName").Value;
                }

                missingDependencies.Add(dependency);
            }
            return missingDependencies;
        }
        #endregion

    }
}
