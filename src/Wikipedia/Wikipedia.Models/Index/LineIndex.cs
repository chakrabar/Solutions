﻿using System.Collections.Generic;

namespace Wikipedia.Models.Index
{
    public class LineIndex
    {
        public int Id { get; set; }
        public string Line { get; set; }
        public ICollection<WordFrequency> WordIndex { get; set; }
    }
}
