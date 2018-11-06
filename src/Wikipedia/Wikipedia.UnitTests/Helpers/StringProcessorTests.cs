using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Wikipedia.Helpers;

namespace Wikipedia.UnitTests.Helpers
{
    [TestClass]
    public class StringProcessorTests
    {
        [TestMethod]
        public void GetWordsLower_should_get_all_words()
        {
            var sentence = @"There are three species of zebras: the plains zebra, the Grévy's zebra and the mountain zebra.";
            
            var d = StringProcessor.GetWordsLower(sentence);

            Assert.IsTrue(d.Count() == 16);
        }
    }
}
