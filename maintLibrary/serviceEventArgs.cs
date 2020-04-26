using System;
using System.Collections.Generic;
using System.Text;

namespace maintLibrary
{
    public class serviceEventArgs : EventArgs
    {
        string owner; string name; string location; string additionalArgs;
        DateTime start; DateTime finish; TimeSpan duration;
        int returnCode;

        public serviceEventArgs(string owner, string name, string location, string additionalArgs, DateTime start, DateTime finish, TimeSpan duration, int returnCode)
        {
            this.owner = owner; this.name = name;this.location = location;this.additionalArgs = additionalArgs;this.start = start;this.finish = finish;this.duration = duration;this.returnCode = returnCode;
        }
    }
}
