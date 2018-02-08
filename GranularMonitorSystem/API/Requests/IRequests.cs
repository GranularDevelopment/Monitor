using System.Threading.Tasks;

namespace GranularMonitorSystem.Services.API.Requests
{
    public interface IRequests
    {
        Task<TResult> GetAsync<TResult>(string uri);

        Task<TResult> PostAsync<TResult>(string uri, TResult data,  string header = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string header = "");

        Task DeleteAsync(string uri);

        Task<TResult> LoginAsync<TResult>(string uri, string username ="", string password="");
    }
}
