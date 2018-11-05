using System.Collections.Generic;

namespace Wikipedia.Models.Index
{
    public class ContentIndex
    {
        public string Content { get; set; }
        public ICollection<LineIndex> Lines { get; set; }
        public ICollection<WordFrequency> AllWords { get; set; }
    }
}
