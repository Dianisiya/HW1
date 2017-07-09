using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using Wheather.Services.Interfaces;

namespace Wheather.Services.Implementations
{
    public class RequestService : IRequestService
    {
        public T ExecuteGetRequest<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var res = default(T);
                try
                {
                    var responce = client.GetStringAsync(url).Result;
                    res = JsonConvert.DeserializeObject<T>(responce);
                }
                catch(HttpRequestException exception) { }
                return res;
            }
        }
    }
}