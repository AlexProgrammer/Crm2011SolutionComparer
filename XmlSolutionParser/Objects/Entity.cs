using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Entity
    {
        public int ObjectTypeCode { get; set; }
        public string LogicalName { get; set; }
        public Label Name { get; set; }
        public Label CollectionName { get; set; }
        public Label Description { get; set; }

    }
}
