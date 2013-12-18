using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HARReplayer
{
    public class Stats
    {
        public Stats()
        {
            Errors = new List<string>();
        }

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public int RequestsExecuted { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public decimal RequestsPerSecond { get; set; }
        public List<String> Errors { get; set; }
    }
}
