using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Configs
{
    public static class InputsConfigs
    {
        public const int PhoneLength = 10;

        //the minimum and maximum length of most generic inputs
        public const int MinGenInputLength = 2;
        public const int MaxGenInputLength = 20;

        //the minimum and maximum length of clients's ids
        public const int MinIdLength = 8;
        public const int MaxIdLength = 10;

        //min and max minute per call simulator
        public const int SimulatorMinMinute = 0;
        public const int SimulatorMaxMinute = 300;
    }
}
