using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.ViewModel;
using Eventmaker.Converter;


namespace Eventmaker.Handler
{
    class EventHandler
    {
        private DateTimeOffset _offset;
        private TimeSpan _timeSpan;
        private DateTime _dateTime;


        //connection both ways between the handler and the viewmodel
        public EventViewModel EventViewModel { get; set; }
        
        //ctorp+TAB
        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }
        
        public void CreateEvent()
        {
            _offset = EventViewModel.Date;
            _timeSpan = EventViewModel.Time;

            _dateTime = DateTimeConverter.DateTimeOffsetAndDateTime(_offset, _timeSpan);

            EventViewModel.EventCatalog.AddEvent(EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, _dateTime);
            
        }
    }
}
