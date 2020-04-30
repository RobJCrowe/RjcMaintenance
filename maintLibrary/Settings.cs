using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace maintLibrary
{
    public class Settings
    {

        static string currDir; static public bool testDB;
        static string settingsFilename = @"\settings.json";
        [JsonProperty]
        public ObservableCollection<service> services { get; set; }
        // other variables

        public Settings()
        {
            try
            {
                currDir = Directory.GetCurrentDirectory();
                //SampleServices();
            }
            catch (Exception)
            {
                //Log issue
                //throw exception;
            }
        }

        public static Settings GetSettings()
        {
            try
            {
                currDir = Directory.GetCurrentDirectory();
            if (File.Exists(currDir + settingsFilename))
                {
                    string input = File.ReadAllText(currDir + settingsFilename);
                    Settings tempSettings = JsonConvert.DeserializeObject<Settings>(input);
                    return tempSettings;
                }
             return new Settings();
            }
            catch(Exception e){ return null; /* log- Console.WriteLine("Reading settings file failed."); */ }
        }
        
        public void WriteSettings()
        {
            try
            {

            }
            catch (Exception)
            {
                // log Console.WriteLine("Writting settings failed);
            }
            string output = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(currDir+settingsFilename, output);
        }

        public bool addService(service s)
        {
                try { services.Add(s); return true; }
                catch (Exception) { return false; }
        }

        public bool editService(service s, int index)
        {
            try { services.Insert(index, s); services.RemoveAt(index + 1); return true; }
            catch (Exception) { return false; }
        }

        private void SampleServices()
        {
            service service01 = new service();
            service01.Name = "service01"; service01.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service01.additionalArgs = "1 service01 true";
            services.Add(service01);
            service service02 = new service();
            service02.Name = "service02"; service02.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service02.additionalArgs = "2 service02";
            services.Add(service02);
            service service03 = new service();
            service03.Name = "service03"; service03.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service03.additionalArgs = "3 service03";
            services.Add(service03);
            service service04 = new service();
            service04.Name = "service04"; service04.location = @"M:\repo\SleepRjc\SleepRjc\bin\Debug\netcoreapp3.1\SleepRjc.exe"; service04.additionalArgs = "4 service04";
            services.Add(service04);
        }
    }
}
