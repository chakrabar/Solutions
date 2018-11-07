using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikipedia.Core;

namespace Wikipedia.IntegrationTests
{
    [TestClass]
    public class CompleteFlowTests
    {
        [TestMethod]
        public void App_should_parse_input_and_generate_output()
        {
            var inputData = TestData.SampleInput;

            var answers = new WikiSolutionFacade()
                .GetAnswersInOrder(inputData);

            Assert.IsNotNull(answers);
            for (int i = 0; i < TestData.CorrectAnswers.Length; i++)
            {
                Assert.AreEqual(TestData.CorrectAnswers[i], answers[i]);
            }
        }
    }
}
