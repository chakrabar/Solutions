using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts.Strategies;
using Wikipedia.Models;
using Wikipedia.Models.Index;

namespace Wikipedia.Core.Strategies
{
    public class LineRankerOnKeywords : ILineMatchRanker
    {
        public IEnumerable<LineRank> GetRelevantLinesWithRank(IEnumerable<WordPriority> keywords, IEnumerable<LineIndex> lines)
        {
            var allLinesWithInitialRank = new List<LineRank>();

            foreach (var term in keywords)
            {
                var allLinesWithTerm = lines.Where(l => l.WordIndex.Any(w => w.Word == term.Word));
                foreach (var match in allLinesWithTerm)
                {
                    allLinesWithInitialRank.Add(new LineRank
                    {
                        LineId = match.Id,
                        Line = match.Line,
                        Score = match.WordIndex.First(wi => wi.Word == term.Word).Frequency * term.Priority
                    });
                }
            }

            var linesWithAgreegatedRank = allLinesWithInitialRank
                .GroupBy(l => l.LineId)
                .Select(g => new LineRank
                {
                    LineId = g.Key,
                    Line = g.First().Line,
                    Score = g.Sum(sameLine => sameLine.Score)
                });

            var result = linesWithAgreegatedRank.Where(lr => lr.Score > 0);

            return result;
        }
    }
}
