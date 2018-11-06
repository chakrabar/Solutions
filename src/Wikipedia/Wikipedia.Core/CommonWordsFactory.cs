namespace Wikipedia.Core
{
    public class CommonWordsFactory
    {
        public static string[] GetQuestionWords()
        {
            return new[] { "what", "when", "why", "where", "which", "whose", "who", "is", "are", "will", "shall", "be" };
        }

        public static string[] GetFrequentWords()
        {
            return new[] { "a", "an", "the", "on", "of", "for", "and", "or" };
        }
    }
}
