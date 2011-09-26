using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Label
    {
        public LocalizedLabel UserLocalizedLabel
        {
            get
            {
                return labels.ContainsKey(this.DefaultLanguageCode) ? labels[DefaultLanguageCode] : null;
            }
        }
        public int DefaultLanguageCode { get; set; }
        private Dictionary<int, LocalizedLabel> labels;

        public Label(int defaultLanguageCode)
            :this()
        {
            this.DefaultLanguageCode = defaultLanguageCode;
        }

        public Label()
        {
            labels = new Dictionary<int, LocalizedLabel>();
        }

        public void AddLocalizedLabel(int languageCode, string text)
        {
            AddLocalizedLabel(new LocalizedLabel()
            {
                LanguageCode = languageCode,
                Value = text
            });
        }

        public void AddLocalizedLabel(LocalizedLabel label)
        {
            labels.Add(label.LanguageCode, label);
        }

        public void AddLocalizedLabels(IEnumerable<LocalizedLabel> labels)
        {
            Parallel.ForEach(labels, (localizedLabel) => AddLocalizedLabel(localizedLabel));
        }

        public LocalizedLabel GetLocalizedLabel(int languageCode)
        {
            return labels.ContainsKey(languageCode) ? labels[languageCode] : null;
        }

    }

    public class LocalizedLabel
    {
        public int LanguageCode { get; set; }
        public string Value { get; set; }
    }
}
