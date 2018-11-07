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
            _paragraph = TestData.Wiki;
            _questions = TestData.Questions;
            _correctAnswers = TestData.CorrectAnswers;
            _answers = TestData.Answers;
            _answerFinder = new AnswerFinder();
            _contentIndex = new ContentIndexBuilder().Build(_paragraph);            
        }

        //intentioanlly keeping separate test cases for better understanding
        [TestMethod]
        public void App_should_find_best_answer_1()
        {
            Should_find_best_answer(0);
        }

        [TestMethod]
        public void App_should_find_best_answer_2()
        {
            Should_find_best_answer(1);
        }

        [TestMethod]
        public void App_should_find_best_answer_3()
        {
            Should_find_best_answer(2);
        }

        [TestMethod]
        public void App_should_find_best_answer_4()
        {
            Should_find_best_answer(3);
        }

        [TestMethod]
        public void App_should_find_best_answer_5()
        {
            Should_find_best_answer(4);
        }

        public void Should_find_best_answer(int index)
        {
            var question = _questions[index];
            var expectedAnswer = _correctAnswers[index];

            var answer = _answerFinder.FindBestAnswer(_contentIndex, question, _answers);

            Assert.AreEqual(expectedAnswer, answer);
        }
    }
}
