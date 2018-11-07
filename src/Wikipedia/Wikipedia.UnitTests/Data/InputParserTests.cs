using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wikipedia.Data;
using Wikipedia.IntegrationTests;
using Wikipedia.Models;

namespace Wikipedia.UnitTests.Data
{
    public class InputParserTests
    {
        [TestClass]
        public class StringProcessorTests
        {
            [TestMethod]
            public void Should_read_all_parts_of_input()
            {
                var inputData = TestData.SampleInput;

                WikiInput parsedData = InputParser.Parse(inputData);

                Assert.IsNotNull(parsedData);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(parsedData.Paragraph));
                Assert.IsNotNull(parsedData.Answers);
                Assert.IsTrue(parsedData.Answers.Count == 5);
                Assert.IsTrue((parsedData.Answers?.Count ?? 0) == 5);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Throws_if_invalid_input_data()
            {
                var incompleteData = new[] { TestData.Wiki };
                WikiInput parsedData = InputParser.Parse(incompleteData);
            }
        }
    }
}
