using System.Collections.Generic;
using System.Linq;
using Wikipedia.AppContracts;
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
                        Rank = match.WordIndex.First(wi => wi.Word == term.Word).Frequency * term.Priority
                    });
                }
            }

            var linesWithAgreegatedRank = allLinesWithInitialRank
                .GroupBy(l => l.LineId)
                .Select(g => new LineRank
                {
                    LineId = g.Key,
                    Line = g.First().Line,
                    Rank = g.Sum(sameLine => sameLine.Rank)
                });

            var result = linesWithAgreegatedRank.Where(lr => lr.Rank > 0);

            return result;
        }
    }
}
