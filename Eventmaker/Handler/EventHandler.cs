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

        public EventHandler()
        {
           EventViewModel = new EventViewModel();
        }

        
        // Not sure about the method below step L) in the SWCLectureDrive
        public void TriggerCreateEventMethod()
        {
            EventViewModel.CreateEvent();
        }
    }
}
