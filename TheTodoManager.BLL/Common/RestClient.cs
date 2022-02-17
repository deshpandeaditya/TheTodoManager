using System.Net.Http;
using System.Threading.Tasks;

namespace TheTodoManager.BLL.Common
{
    public static class RestClient
    {
        private static HttpClient Client = new();
        public static Task<HttpResponseMessage> Get(string uri)
        {
            var response = Client.GetAsync(uri);
            return response;
        }
    }
}
