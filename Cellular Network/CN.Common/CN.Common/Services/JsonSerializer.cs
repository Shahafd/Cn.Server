using CN.Common.Contracts.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string SerializeObject(T ToSer)
        {
            return JsonConvert.SerializeObject(ToSer);
        }

        public T DeSerializeObject(string ToDes)
        {
            return JsonConvert.DeserializeObject<T>(ToDes);
        }
    }
}
