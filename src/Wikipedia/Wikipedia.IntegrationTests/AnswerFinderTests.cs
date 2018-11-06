using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wikipedia.Core;
using Wikipedia.Models.Index;

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
        ContentIndex _contentIndex = null;

        [TestInitialize]
        public void Setup()
        {
            _paragraph = TestConstants.Wiki;
            _questions = TestConstants.Questions;
            _answerFinder = new AnswerFinder();
            _contentIndex = new ContentIndexBuilder().Build(_paragraph);
            _correctAnswers = new[]
            {
                @"Grévy's zebra and the mountain zebra",
                @"aims to breed zebras that are phenotypically similar to the quagga",
                @"horses and donkeys",
                @"the plains zebra, the Grévy's zebra and the mountain zebra",
                @"subgenus Hippotigris"
            };
        }

        //intentioanlly keeping separate test cases for better understanding
        [TestMethod]
        public void App_should_find_best_answer_1()
        {
            App_should_find_best_answer(0);
        }

        [TestMethod]
        public void App_should_find_best_answer_2()
        {
            App_should_find_best_answer(1);
        }

        [TestMethod]
        public void App_should_find_best_answer_3()
        {
            App_should_find_best_answer(2);
        }

        [TestMethod]
        public void App_should_find_best_answer_4()
        {
            App_should_find_best_answer(3);
        }

        [TestMethod]
        public void App_should_find_best_answer_5()
        {
            App_should_find_best_answer(4);
        }

        public void App_should_find_best_answer(int index)
        {
            var question = _questions[index];
            var expectedAnswer = _correctAnswers[index];

            var answer = _answerFinder.FindBestAnswer(_contentIndex, question, _answers);

            Assert.AreEqual(expectedAnswer, answer);
        }
    }
}
