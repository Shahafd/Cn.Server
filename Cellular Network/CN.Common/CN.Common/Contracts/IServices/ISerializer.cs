using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface ISerializer<T>
    {
        T DeSerializeObject(string ToDes);
        string SerializeObject(T ToSer);
    }
}
