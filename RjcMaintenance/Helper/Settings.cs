using System;
using System.Collections.Generic;
using System.Text;
using RjcMaintenance.Helper;

namespace RjcMaintenance
{
    class Settings
    {
        string currDir;
        List<service> services = new List<service>();
        // other variables

        public Settings()
        {
            try
            {
                SampleServices();
                
                //open file
                //parse file add to services as necessary
            }
            catch (Exception)
            {
                //Log issue
                //throw exception;
            }
        }

        private void SampleServices() 
        {
            service service01 = new service();
            service01.name = "service01"; service01.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service01.additionalArgs = "1 service01";
            services.Add(service01);
            service service02 = new service();
            service02.name = "service02"; service02.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service02.additionalArgs = "2 service02";
            services.Add(service02);
            service service03 = new service();
            service03.name = "service03"; service03.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service03.additionalArgs = "3 service03";
            services.Add(service03);
            service service04 = new service();
            service04.name = "service04"; service04.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service04.additionalArgs = "4 service04";
            services.Add(service04);
        }
       
        public List<service> GetServices(){ return services;}
    }
}
