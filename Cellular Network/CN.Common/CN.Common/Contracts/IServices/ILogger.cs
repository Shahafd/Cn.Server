using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts
{
    public interface ILogger
    {
        void Print(string message);
        void PrintList(List<string> messages);
    }
}
