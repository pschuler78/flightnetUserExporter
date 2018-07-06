using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightNetUserExporter
{
    public class LogEventArgs : EventArgs
    {
        public string Text { get; set; }
        public LogEventArgs(string text)
        {
            Text = text;
        }
    }
}
