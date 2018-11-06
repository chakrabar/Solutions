namespace Wikipedia.Core
{
    public class CommonWordsStore
    {
        public static string[] GetQuestionWords()
        {
            return new[] { "what", "when", "why", "where", "which", "whose", "who", "is", "are", "will", "shall", "be", "do", "does", "can", "may", "might" };
        }

        public static string[] GetFrequentWords()
        {
            return new[] { "a", "an", "the", "on", "of", "for", "and", "or", "to", "by" };
        }
    }
}
