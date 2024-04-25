using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Model
{
    public class Workplace
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Workplace(string name, string address) 
        {
            this.Name = name;
            this.Address = address;
        }
    }
}
