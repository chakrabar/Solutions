using System;
using System.Collections.Generic;
using System.Linq;
using Wikipedia.Models;

namespace Wikipedia.Data
{
    public class InputParser
    {
        public WikiInput Parse(IEnumerable<string> input)
        {
            if (input == null || input.Count() != 7)
                throw new ApplicationException("Invalid input data");

            return new WikiInput
            {
                Paragraph = input.First(),
                Questions = input.Skip(1).Take(5).ToList(),
                Answers = input.Last()
                    .Split(";")
                    .Select(p => p.Trim())
                    .ToList()
            };
        }
    }
}
