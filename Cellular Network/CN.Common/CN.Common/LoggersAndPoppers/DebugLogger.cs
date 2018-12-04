using CN.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.LoggersAndPoppers
{
    public class DebugLogger : ILogger
    {
        public void Print(string message)
        {
            Debug.Write(message);
        }

        public void PrintList(List<string> messages)
        {
            string mainMsg = "";
            foreach (var item in messages)
            {
                mainMsg += item + Environment.NewLine;
            }
            if (!string.IsNullOrWhiteSpace(mainMsg))
            {
                Debug.WriteLine(mainMsg);
            }
        }
    }
}
