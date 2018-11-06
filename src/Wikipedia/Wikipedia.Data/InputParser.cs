using System;
using System.Collections.Generic;
using System.Linq;
using Wikipedia.Helpers;
using Wikipedia.Models;

namespace Wikipedia.Data
{
    public class InputParser
    {
        public static WikiInput Parse(IEnumerable<string> input)
        {
            if (input == null || input.Count() != 7)
                throw new ArgumentException("Invalid input data");

            return new WikiInput
            {
                Paragraph = input.First(),
                Questions = input.Skip(1).Take(5).ToList(),
                Answers = StringProcessor.GetAllParts(input.Last(), ';').ToList()
            };
        }
    }
}
