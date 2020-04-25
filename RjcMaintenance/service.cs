using RjcMaintenance.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RjcMaintenance
{
    class service
    {
        internal string owner, name, location, additionalArgs;
        internal DateTime start, finish;
        internal TimeSpan duration;
        internal int returnCode;
        public event EventHandler<serviceEventArgs> startEvent; 
        public event EventHandler<serviceEventArgs> endEvent;
        public service() { }
        public service(string name){ this.name = name; }

        private void write(string s) { Console.WriteLine(s); }
        
        public static void ExecuteServices(List<service> services)
        {
            foreach (var s in services)
            {
                s.start = DateTime.Now;
                s.Execute();
                s.finish = DateTime.Now;
                s.duration = s.finish.Subtract(s.start);
            }
            // sub to logger
            // unsub logger
        }

        public void Execute() //cf https://stackoverflow.com/a/10072082
        {
            var process = new Process();
            this.startEvent(this, new serviceEventArgs(this.owner, this.name, this.location, this.additionalArgs, this.start, this.finish, this.duration, this.returnCode));
            process.StartInfo.FileName = this.location;
            if (!string.IsNullOrEmpty(this.additionalArgs)) { process.StartInfo.Arguments = this.additionalArgs; }
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.UseShellExecute = true;

            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                this.returnCode = process.ExitCode;
                this.endEvent(this, new serviceEventArgs(this.owner, this.name, this.location, this.additionalArgs, this.start, this.finish, this.duration, this.returnCode));
            }
            catch (Exception e) { throw new Exception("OS error while executing " + this.name + ": " + e.Message, e); }
        }
    }
}
