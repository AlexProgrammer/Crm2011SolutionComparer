using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class PublisherAddress
    {
        public PublisherAddress()
        {
            this.FreightTermsCode = null;
            this.ImportSequenceNumber = null;
        }

        public int AddressNumber { get; set; }
        public int AddressTypeCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Fax { get; set; }

        public int? FreightTermsCode { get; set; }
        public int? ImportSequenceNumber { get; set; }

        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string PostOfficeBox { get; set; }
        public string PrimaryContactName { get; set; }

        public int ShippingMethodCode { get; set; }

        public string StateOrProvince { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }

        public string TimeZoneRuleVersionNumber { get; set; }
        public string UPSZone { get; set; }
        public string UTCOffset { get; set; }
        public string UTCConversionTimeZoneCode { get; set; }

    }
}
