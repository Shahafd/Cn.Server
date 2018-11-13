using CN.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CN.Common.LoggersAndPoppers
{
    public class MessageBoxPopper : ILogger
    {
        public void Print(string message)
        {
            MessageBox.Show(message);
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
                MessageBox.Show(mainMsg);
            }
        }
    }

}
