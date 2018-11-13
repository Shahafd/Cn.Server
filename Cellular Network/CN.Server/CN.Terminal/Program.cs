using CN.Common.Contracts.Signalr;
using CN.DAL.Databases;
using CN.Terminal.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Terminal
{
    class Program
    {

        static void Main(string[] args)
        {
            CnContext context = new CnContext();
            Console.WriteLine("Server Online");

            Console.ReadLine();
        }
    }
}
