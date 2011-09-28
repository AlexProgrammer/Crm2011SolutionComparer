using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Alex.Net.Crm.SolutionCompare.Parser.Objects;

namespace Alex.Net.Crm.SolutionCompare.Parser
{
    internal class Util
    {
        protected static XNamespace xsiNameSpace = "http://www.w3.org/2001/XMLSchema-instance";

        internal static string GetElementValueOrNull(XElement element)
        {
            return element.Attribute(xsiNameSpace + "nil") != null && element.Attribute(xsiNameSpace + "nil").Value.Equals("true") ? null : element.Value;
        }

        internal static Label ParseLocalizedLabelElement(XElement localizedElement, int defaultLanguageCode)
        {
            Label label = new Label(defaultLanguageCode);
            label.AddLocalizedLabels(
                from e in localizedElement.Elements()
                select new LocalizedLabel()
                {
                    Value = e.Attribute("description").Value,
                    LanguageCode = int.Parse(e.Attribute("languagecode").Value)
                });
            return label;
        }
    }
}
