namespace Wikipedia.Models
{
    public class AnswerRank
    {
        public string Answer { get; set; }
        public int WordMatchScore { get; set; }
        public decimal WeightedScore { get; set; }
    }
}
