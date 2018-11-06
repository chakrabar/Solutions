using Wikipedia.AppContracts;
using Wikipedia.Data;
using Wikipedia.Models;

namespace Wikipedia.Core
{
    public class AnswerFinderFacade
    {
        private readonly IContentIndexBuilder _contentIndexBuilder;
        private readonly IAnswerFinder _answerFinder;

        public AnswerFinderFacade()
            : this(new ContentIndexBuilder(), new AnswerFinder())
        { }
        public AnswerFinderFacade(IContentIndexBuilder contextIndexer, IAnswerFinder answerFinder)
        {
            _answerFinder = answerFinder;
            _contentIndexBuilder = contextIndexer;
        }

        public string[] GetAnswersInOrder(string[] rawInput)
        {
            WikiInput parsedInput = InputParser.Parse(rawInput);

            var indexedData = _contentIndexBuilder.Build(parsedInput.Paragraph);

            var answers = new string[5];
            for (int i = 0; i < parsedInput.Questions.Count; i++)
            {
                answers[i] = _answerFinder.FindBestAnswer(indexedData,
                    parsedInput.Questions[i], parsedInput.Answers);
            }

            return answers;
        }
    }
}
