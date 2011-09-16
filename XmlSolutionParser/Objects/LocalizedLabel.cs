using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class LocalizedLabel
    {
        private Dictionary<int, Label> labels;

        public LocalizedLabel()
        {
            labels = new Dictionary<int, Label>();
        }

        public void AddLabel(int languageCode, string text)
        {
            AddLabel(new Label()
            {
                LanguageCode = languageCode,
                Text = text
            });
        }

        public void AddLabel(Label label)
        {
            labels.Add(label.LanguageCode, label);
        }

        public Label GetLocalizedLabel(int languageCode)
        {
            return labels.ContainsKey(languageCode) ? labels[languageCode] : null;
        }
    }

    public class Label
    {
        public int LanguageCode { get; set; }
        public string Text { get; set; }
    }
}
