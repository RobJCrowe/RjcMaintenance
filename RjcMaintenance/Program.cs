using RjcMaintenance.Helper;
using System;

namespace RjcMaintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings(); // grab settings file and parse into object/list method
            // create logger instance

            service.ExecuteServices(settings.GetServices());
            

            // Run report

        }
    }
}
