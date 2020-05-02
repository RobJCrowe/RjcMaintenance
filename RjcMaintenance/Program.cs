using RjcMaintenance.Helper;
using System;
using maintLibrary;
using System.Collections.Generic;

namespace RjcMaintenance
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Settings settings = Settings.GetSettings(); // grab settings file and parse into object/list method
            if(settings.testDB == true) { dbHelper.testDb(); Environment.Exit(0); }
            // create logger instance

            //List<service> temp = settings.GetServices();
            service.ExecuteServices(settings);
            
            //settings.WriteSettings();

            // Run report

        }
    }
}
