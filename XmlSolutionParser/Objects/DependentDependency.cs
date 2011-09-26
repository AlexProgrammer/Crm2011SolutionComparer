using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class DependentDependency
    {
        public Guid Id { get; set; }
        public int Key { get; set; }
        public ComponentType Type { get; set; }
        public string ParentDisplayName { get; set; }

    }
}
