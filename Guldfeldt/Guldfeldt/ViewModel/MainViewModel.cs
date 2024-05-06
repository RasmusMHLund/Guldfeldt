using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.ViewModel
{
    internal class MainViewModel
    {
        public EmployeeViewModel evm;
        public WorkplaceViewModel wvm;
        public NoteViewModel nvm;
        public MainViewModel(EmployeeViewModel evm, WorkplaceViewModel wvm, NoteViewModel nvm)
        {
            this.evm = evm;
            this.wvm = wvm;
            this.nvm = nvm;
        }
    }   
}
