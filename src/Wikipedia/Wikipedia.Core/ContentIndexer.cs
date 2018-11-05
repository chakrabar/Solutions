using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Wikipedia.Models.Index;

namespace Wikipedia.Core
{
    public class ContentIndexer
    {
        public ContentIndex Build(string paragraph)
        {
            var result = new ContentIndex
            {
                Content = paragraph,
                Lines = new List<LineIndex>(),
                AllWords = new List<WordFrequency>()
            };

            var lines = paragraph
                .Split(".")
                .Select(l => l.Trim());
            foreach (var line in lines)
            {
                var words = Regex.Split(line, @"\b[\w']*\b");
            }
        }

        private 
    }
}
