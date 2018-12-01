using System.Threading.Tasks;

namespace AppContracts
{
    public interface IWebClient
    {
        Task<string> GetWebData(string url);
    }
}
