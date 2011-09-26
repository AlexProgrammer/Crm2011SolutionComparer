using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Dependency
    {
        public RequiredDependency Required { get; private set; }
        public DependentDependency Dependent { get; private set; }

        public Dependency()
        {
            this.Required = new RequiredDependency();
            this.Dependent = new DependentDependency();
        }
    }
}
