using System.Collections.Generic;

namespace Wikipedia.Models
{
    public class WikiInput
    {
        public string Paragraph { get; set; }
        public IList<string> Questions { get; set; }
        public IList<string> Answers { get; set; }
    }
}
