using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Wheather.Services.Interfaces;
using System.Threading.Tasks;

namespace Wheather.Services.Implementations
{
    public class RequestService : IRequestService
    {
        public async Task<T> ExecuteGetRequest<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var res = default(T);
                try
                {
                    var responce = await client.GetStringAsync(url);
                    res = JsonConvert.DeserializeObject<T>(responce);
                }
                catch (HttpRequestException exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                return res;
            }
        }
    }
}