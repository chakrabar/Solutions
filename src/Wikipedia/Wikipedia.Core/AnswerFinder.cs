﻿using System.Linq;
using Wikipedia.AppContracts;
using Wikipedia.Core.Strategies;
using Wikipedia.Helpers;
using Wikipedia.Models.Index;

namespace Wikipedia.Core
{
    public class AnswerFinder
    {
        private readonly IKeywordPrioritizer _keywordPrioritizer;
        private readonly ILineMatchRanker _lineMatchRanker;
        private readonly IAnswerRanker _answerRanker;

        public AnswerFinder()
            : this(new WordPrioritizer(), new LineRankerOnKeywords(), new AnswerRankerOnLine())
        { }
        public AnswerFinder(IKeywordPrioritizer keywordPrioritizer,
            ILineMatchRanker lineMatchRanker,
            IAnswerRanker answerRanker)
        {
            _keywordPrioritizer = keywordPrioritizer;
            _lineMatchRanker = lineMatchRanker;
            _answerRanker = answerRanker;
        }

        public string FindBestAnswer(ContentIndex indexData, string question, string[] answers)
        {
            var questionTermWithPriority = _keywordPrioritizer.GetWordsWithPriority(indexData, question);

            var relevantLinesWithRank = _lineMatchRanker
                .GetRelevantLinesWithRank(questionTermWithPriority, indexData.Lines);

            var predictedLine = relevantLinesWithRank
                .MaxBy(lr => lr.Score)
                .Line;

            var answersWithRank = _answerRanker.GetWordMatchRanks(predictedLine, answers);

            var predictedAnswer = answersWithRank
                .MaxBy(ar => ar.WeightedScore); //.OrderByDescending(a => a.WordMatch).ThenByDescending(m => m.WeightedScore).First();

            return predictedAnswer.Answer;
        }
    }
}