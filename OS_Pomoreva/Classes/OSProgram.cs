using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OS_Pomoreva.Classes
{
    public class OSProgram
    {
        public string name { get; }
        public int threads { get; }
        public int memory { get; }
        public int clockCycle { get; }
        public int priority { get; } // от 1 до 10
        public string modules { get; }

        public Bitmap icon { get; }

        public OSProgram (string name, int threads, int memory, int clockCycle, int priority,Bitmap icon, string modules)
        {
            this.name = name;
            this.threads = threads;
            this.memory = memory;
            this.clockCycle = clockCycle;
            this.priority = priority;
            this.icon = icon;
            this.modules = modules;
        }
        public OSProgram(string name,int clockCycle,Bitmap icon)
        {
            this.name = name;
            this.clockCycle = clockCycle;
            this.icon = icon;
        }
    }
}
