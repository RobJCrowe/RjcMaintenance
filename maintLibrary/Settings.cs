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
        [JsonProperty]
        public bool testDB = false, logToDb = false;  public string password;
        static string currDir; 
        static string settingsFilename = @"\settings.json";
        [JsonProperty]
        public ObservableCollection<service> services { get; set; }
        public Dictionary<string, string> presetList = new Dictionary<string, string>();
        // other variables

        public Settings()
        {
            try
            {
                currDir = Directory.GetCurrentDirectory();
                generatePresets();
                //SampleServices();
            }
            catch (Exception)
            {
                //Log issue
                //throw exception;
            }
        }

        private void generatePresets()
        {
            presetList.Add("Auslogic Disk Defrag", @" {driveLetter}: -o");
            presetList.Add("CCleaner", @" /Auto");
            presetList.Add("FreeFileSync", @" ""path.ffs_batch""");
            presetList.Add("Macrium Reflect Drive Image", @" -e -w  -full ""path.xml""");
            presetList.Add("Microsoft Malware Removal Tool", @" /quiet /F:Y");
            presetList.Add("Microsoft Security Essentials", @" -Scan -Scantype 2");
        }
        public Dictionary<string, string> getPresets() { return presetList; }
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
        
        public bool WriteSettings()
        {
            try
            {
                foreach (var s in services) { s.owner = password; }
                string output = JsonConvert.SerializeObject(this, Formatting.Indented);
                System.IO.File.WriteAllText(currDir + settingsFilename, output);
                return true;
            }
            catch (Exception)
            {
                // log Console.WriteLine("Writting settings failed);
                return false;
            }
            
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

        public bool removeService(int index)
        {
            try { this.services.RemoveAt(index); return true; }
            catch (Exception) {return false; }
        }

        public int serviceUp(int index)
        {
            if (index != 0)
            {
                services.Move(index, index - 1);
                if (index - 1 == -1) return 0;
                else { return index - 1; }
            }
            else { return 0; }
            
        }
        public int serviceDown(int index)
        {
            if (index != services.Count-1)
            {
                services.Move(index, index + 1);
                if (index + 1 == services.Count) return services.Count;
                else { return index + 1; }
            }
            return services.Count-1; 
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
