using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Eventmaker.ViewModel;
using Eventmaker.Converter;
using Eventmaker.Model;
using Eventmaker.View;
using Microsoft.Xaml.Interactions.Core;


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

            if (EventViewModel.IsValidEvent())
            {
                EventViewModel.EventCatalog.AddEvent
                    (EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, _dateTime);
                //EventViewModel.GoBackToEventsAfterSaving();
            }
            else
            {
                EventValidationErrorMsg();
            }
        }

        public void DeleteEvent()
        {
            DeleteEventErrorMsg();
        }

        public void SetSelectedEvent(Event selectedEvent)
        {
            EventViewModel.SelectedEvent = selectedEvent;
        }

        
        public async void EventValidationErrorMsg()
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
                         "Are you sure that you want to save the Event, "+
                         "without specifying some important details?", "Warning! Empty Field(s)!");

            dialog.Commands.Add (new Windows.UI.Popups.UICommand
                ("Yes")
            {
                Invoked = command => EventViewModel.EventCatalog.AddEvent
                                        (EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, 
                                            EventViewModel.Place, _dateTime),
                Id = 0
            });
            dialog.Commands.Add (new Windows.UI.Popups.UICommand
                ("No, go back")
            {
                Invoked = command => EventViewModel.SetBorderBrushColor(),
                Id = 1
            });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();
            
        }

        public async void DeleteEventErrorMsg()
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
                         "Are you sure that you want to delete the selected Event, " +
                         "this action removes it pernamently from your list!", "Warning! Deleting Event from List!");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand
                ("Yes")
            {
                Invoked = command => EventViewModel.EventCatalog.events.Remove(
                                        EventViewModel.SelectedEvent),
                Id = 0
            });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand
                ("No, go back")
            {
                Id = 1
            });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

        }
    }
}
