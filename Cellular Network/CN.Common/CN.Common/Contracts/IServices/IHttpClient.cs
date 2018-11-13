using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface IHttpClient
    {
        object PostRequest(string route, object obj);
        object GetRequest(string route);
    }
}
