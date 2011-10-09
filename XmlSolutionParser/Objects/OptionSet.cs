using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class OptionSet
    {
        public Label DisplayName { get; set; }
        public Label Description { get; set; }
        public OptionSetType Type { get; set; }
        public List<Option> Options { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsCustomizable { get; set; }
    }
}
