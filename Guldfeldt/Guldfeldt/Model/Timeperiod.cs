using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Timeperiod
    {
        public int TimeperiodId { get; set; }
        public DateTime? Period { get; set; }
        public Timeperiod(int TimeperiodId, DateTime? period) 
        {
            this.TimeperiodId = TimeperiodId;
            this.Period = period;

        }
        public Timeperiod() : this(0, null) { }
    }
}
