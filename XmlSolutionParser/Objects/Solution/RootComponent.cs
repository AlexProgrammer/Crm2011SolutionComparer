using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class RootComponent
    {
        public ComponentType Type { get; set; }
        public string SchemaName { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public RootComponent()
        {
            this.Id = Guid.Empty;
            this.ParentId = Guid.Empty;
            this.Type = ComponentType.Undefined;
        }
    }
}
