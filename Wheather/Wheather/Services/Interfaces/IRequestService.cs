using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheather.Services.Interfaces
{
    public interface IRequestService
    {
        T ExecuteGetRequest<T>(string url);
    }
}
