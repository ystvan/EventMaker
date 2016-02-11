using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Popups;
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
                EventViewModel.EventCatalog.AddEvent(EventViewModel.Id, EventViewModel.Name, EventViewModel.Description, EventViewModel.Place, _dateTime);
                
            }
            else
            {

                MessageDialogHelper.Show(
                    "The Field(s) cannot be empty, please fill out all necesarry information!",
                    "Missing information!");

            }

        }

        public void DeleteEvent()
        {
            EventViewModel.EventCatalog.events.Remove(EventViewModel.SelectedEvent);
            
        }

        public void SetSelectedEvent(Event selectedEvent)
        {
            EventViewModel.SelectedEvent = selectedEvent;
            
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string header)
            {
                MessageDialog messageDialog = new MessageDialog(content, header);
                await messageDialog.ShowAsync();

            }
        }

        //public void NavigateToEventPage()
        //{

        //    INavigate newNavigate = new Frame();
        //    newNavigate.Navigate(typeof(EventPage));

            
            
        //    NavigateToPageAction newAction = new NavigateToPageAction();

        //    newAction.TargetPage(newNavigate);
            

        //}
    }
}
