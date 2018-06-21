using System.Threading.Tasks;

namespace Monitor.Services.Requests
{
    public interface IRequests
    {
        Task<TResult> GetAsync<TResult>(string uri);

        Task<TResult> PostAsync<TResult>(string uri, TResult data,  string header = "");

        Task<TResult> GetAsync<TResult>(string uri, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string header = "");

        Task DeleteAsync(string uri);

    }
}
