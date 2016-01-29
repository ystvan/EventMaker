using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.ViewModel;

namespace Eventmaker.Handler
{
    class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }
        
        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }
        
        // Not sure about the method below step L) in the SWCLectureDrive
        public void CreateEvent()
        {
           //TODO: implement method here
        }
    }
}
