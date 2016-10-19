using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Traffic_Simulator_V3
{
    /// <summary>
    /// Ši klasė:
    /// 5. Turi nestandartines išimtis;
    /// </summary>
    
    class TestOverloadException : Exception
    {
        string infoMessage = "Maximum capability of program has been reached.";
        string sorryMessage = "We are sorry for inconvenience.";

        public TestOverloadException()
        {
            Console.WriteLine(infoMessage);
            Console.WriteLine(sorryMessage);
            Thread.Sleep(3000);
        }
    }
}
