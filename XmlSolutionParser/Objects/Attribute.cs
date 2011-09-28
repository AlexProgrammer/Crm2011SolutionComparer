using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Attribute
    {
        public string PhysicalName { get; set; }
        public AttributeType Type { get; set; }
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public RequiredLevel RequiredLevel { get; set; }

    }
}
