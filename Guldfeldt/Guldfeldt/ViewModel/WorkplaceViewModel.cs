using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guldfeldt.Model;

namespace Guldfeldt.ViewModel
{
    public class WorkplaceViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public WorkplaceViewModel(Workplace workplace) 
        {
            Name = workplace.Name;
            Address = workplace.Address;
        }
    }
}
