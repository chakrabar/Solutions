using System.Collections.Generic;
using Wikipedia.Models.Index;

namespace Wikipedia.AppContracts
{
    public interface IAnswerFinder
    {
        string FindBestAnswer(ContentIndex indexData, string question, IEnumerable<string> answers);
    }
}
