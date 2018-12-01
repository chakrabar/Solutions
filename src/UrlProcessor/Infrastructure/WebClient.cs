using AppContracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class WebClient : IWebClient
    {
        private readonly HttpClient _client = new HttpClient(); 

        public Task<string> GetWebData(string url)
        {
            return _client.GetStringAsync(url);
        }
    }
}
