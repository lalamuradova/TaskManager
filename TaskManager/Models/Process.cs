using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
   public class Process
    {
        public string Name { get; set; }
        public int HandleCount { get; set; }
        public int ThreadCount { get; set; }
        public string MachineName { get; set; }
    }
}
