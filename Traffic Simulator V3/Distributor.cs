using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traffic_Simulator_V3
{
    static class Distributor
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Command line given, display console
                AllocConsole();
                ConsoleMain(args);

            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            }
            
        }

        private static void ConsoleMain(string[] args)
        {
            Console.WriteLine("Command line = {0}", Environment.CommandLine);
            for (int ix = 0; ix < args.Length; ++ix)
                Console.WriteLine("Argument{0} = {1}", ix + 1, args[ix]);
            Console.WriteLine("privet");
            Console.ReadLine();
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
    }
}