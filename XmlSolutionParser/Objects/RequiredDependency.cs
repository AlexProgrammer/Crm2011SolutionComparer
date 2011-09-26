using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class RequiredDependency
    {
        public Guid Id { get; set; }
        public int Key { get; set; }
        public ComponentType Type { get; set; }
        public string SchemaName { get; set; }
        public string DisplayName { get; set; }
        public string ParentSchemaName { get; set; }
        public string ParentDisplayName { get; set; }
        public string Solution { get; set; }
    }
}
