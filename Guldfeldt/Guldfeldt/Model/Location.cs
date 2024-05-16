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

        public Location(int locationId, string name, string address, bool? isConstructionSite, bool? isSchool) 
        {
            this.LocationId = locationId;
            this.Name = name;
            this.Address = address;
            this.IsConstructionSite = isConstructionSite;
            this.IsSchool = isSchool;
        }
        public Location(): this(0, null, null, false, false) { }
    }
}
