using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;


namespace maintLibrary
{

    public class service : INotifyPropertyChanged
    {
        [JsonProperty]
        public string owner, location, additionalArgs;
        private string name; private bool active;
        public string Name
        {
            get { return name; }
            set { 
                if (value != name)
                    {
                    name = value;
                    NotifyPropertyChanged();
                    }
                }
        }

        public bool Active
        {
            get { return active; }
            set { active = value;
                if (value != active)
                {
                    active = value;
                    NotifyPropertyChanged();
                }
            }
        }

        internal DateTime start, finish;
        internal TimeSpan duration;
        internal int returnCode;
        public event EventHandler<serviceEventArgs> startEvent;
        public event EventHandler<serviceEventArgs> endEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public service() { }
        private void write(string s) { Console.WriteLine(s); }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static void ExecuteServices(Settings settings)
        {
            foreach (var s in settings.services)
            {
                if (s.active == false) { continue; }
                s.Execute();
            }
            // sub to logger
            // unsub logger
        }

        public void Execute() //cf https://stackoverflow.com/a/10072082
        {
            greeting(this);
            var process = new Process();
            process.StartInfo.FileName = this.location;
            if (!string.IsNullOrEmpty(this.additionalArgs)) { process.StartInfo.Arguments = this.additionalArgs; }
            process.StartInfo.CreateNoWindow = true; process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true; process.StartInfo.RedirectStandardError = true;
            StringBuilder stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data);
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                this.returnCode = process.ExitCode;
                exit(this, stdOutput);
            }
            catch (Exception e) { throw new Exception("OS error while executing " + this.name + ": " + e.Message, e); }
        }

        private void greeting(service s)
        {
            if (this.startEvent != null)
            {
                this.startEvent(this, new serviceEventArgs(this.owner, this.name, this.location, this.additionalArgs, this.start, this.finish, this.duration, this.returnCode));
            }
            s.start = DateTime.Now;
            write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:sstt") + " --Starting: " + s.name);
        }
        private void exit(service s, StringBuilder sb)
        {
            s.finish = DateTime.Now;
            s.duration = s.finish.Subtract(s.start);
            write(sb.ToString());
            if (this.endEvent != null)
            {
                this.endEvent(this, new serviceEventArgs(this.owner, this.name, this.location, this.additionalArgs, this.start, this.finish, this.duration, this.returnCode));
            }
            write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:sstt") + " --Finished: " + s.name + " Return Code: " + this.returnCode);
            write(Environment.NewLine);
        }

    }
}
