using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikipedia.Core;

namespace Wikipedia.IntegrationTests
{
    [TestClass]
    public class AnswerFinderTests
    {
        string _paragraph = null;
        string[] _answers = null;
        string[] _questions = null;
        string[] _correctAnswers = null;
        AnswerFinder _answerFinder = null;

        [TestInitialize]
        public void Setup()
        {
            _paragraph = TestConstants.Wiki;
            _answers = TestConstants.AllAnswers;
            _questions = TestConstants.Questions;
            _answerFinder = new AnswerFinder();
            _correctAnswers = new[]
            {
                @"Grévy's zebra and the mountain zebra",
                @"aims to breed zebras that are phenotypically similar to the quagga",
                @"horses and donkeys",
                @"the plains zebra, the Grévy's zebra and the mountain zebra",
                @"subgenus Hippotigris"
            };
        }

        [TestMethod]
        public void App_should_find_best_answer()
        {
            var indexData = new ContentIndexBuilder()
                .Build(_paragraph);

            for (int i = 0; i < 5; i++)
            {
                var question = _questions[i];
                var answer = _answerFinder.FindBestAnswer(indexData, question, _answers);

                Assert.AreEqual(_correctAnswers[i], answer);
            }
        }
    }
}
