using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? IsConstructionSite { get; set; }
        public bool? IsSchool { get; set; }

        public Location(string name, string address) 
        {
            this.Name = name;
            this.Address = address;
        }
        public Location(): this(null, null) { }
    }
}
