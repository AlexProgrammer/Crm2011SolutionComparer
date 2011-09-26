using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class PublisherInfo
    {
        public string UniqueName { get; set; }
        public Label Name { get; set; }
        public Label Description { get; set; }
        public string EmailAddress { get; set; }
        public string SupportSite { get; set; }
        public string CustomizationPrefix { get; set; }
        public int CustomizationOptionValuePrefix { get; set; }

        public PublisherAddress Address1 { get; set; }
        public PublisherAddress Address2 { get; set; }

        public PublisherInfo()
        {
            this.Address1 = new PublisherAddress();
            this.Address2 = new PublisherAddress();
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", this.Name.UserLocalizedLabel, this.UniqueName);
        }
    }
}
