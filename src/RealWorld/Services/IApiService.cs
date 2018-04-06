using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealWorld.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null);

        Task<T> PostAsync<T>(string apiUrl, object content = null, bool autheticate = false, IDictionary<string, string> queryParams = null);

        Task<T> PutAsync<T>(string apiUrl, object content = null, bool autheticate = false, IDictionary<string, string> queryParams = null);

        Task<T> DeleteAsync<T>(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null);

        Task DeleteAsync(string apiUrl, bool autheticate = false, IDictionary<string, string> queryParams = null);

    }
}
