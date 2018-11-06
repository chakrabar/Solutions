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
            var inputData = TestConstants.SampleInput;

            var answers = new AnswerFinderFacade()
                .GetAnswersInOrder(inputData);

            Assert.IsNotNull(answers);
            for (int i = 0; i < TestConstants.CorrectAnswers.Length; i++)
            {
                Assert.AreEqual(TestConstants.CorrectAnswers[i], answers[i]);
            }
        }
    }
}
