using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.ViewModel
{
    internal class MainViewModel
    {
        public ApprenticeViewModel avm;
        public JourneymanViewModel jvm;
        public WorkplaceViewModel wvm;
        public NoteViewModel nvm;
        public MainViewModel(ApprenticeViewModel avm, JourneymanViewModel jvm, WorkplaceViewModel wvm, NoteViewModel nvm)
        {
            this.avm = avm;
            this.jvm = jvm;
            this.wvm = wvm;
            this.nvm = nvm;
        }
    }   
}
