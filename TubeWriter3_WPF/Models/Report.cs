using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubeWriter3_WPF.Models
{
    public class Report
    {
        public bool Boolean { get; set; }
        public string Message { get; set; }

        public Report(bool boolean, string message = "Unknown error.")
        {
            Boolean = boolean;
            Message = message;
        }
    }
}
