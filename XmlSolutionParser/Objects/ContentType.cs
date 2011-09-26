using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public enum ContentTypes
    {
        Default,
        Override
    }

    public class ContentType
    {
        public ContentTypes Type { get; set; }
        public string Extension { get; set; }
        public string ContentStreamType { get; set; }
        public string PartName { get; set; }
    }
}
