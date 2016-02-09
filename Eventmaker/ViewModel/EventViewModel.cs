using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Eventmaker.Annotations;
using Eventmaker.Common;
using Eventmaker.Converter;
using Eventmaker.Handler;
using Eventmaker.Model;
using Eventmaker.View;

namespace Eventmaker.ViewModel
{


    class EventViewModel : INotifyPropertyChanged
    {
        // private backing fields:
        private int id;
        private string name;
        private string description;
        private string place;
        private DateTimeOffset date;
        private TimeSpan time;
        private ICommand _createEventCommand;

        // 2 References from another classes:
        public EventCatalogSingleton EventCatalog { get; set; }

        // someone like the controllar pattern, it handles the class
        public Handler.EventHandler EventHandler { get; set; }
        
        //ObservableCollection getter and setter
        public ObservableCollection<Event> Events
        {
            get { return EventCatalog.events; }

            set { EventCatalog.events = value; }
        }

        // public properties:
        public int Id
        {
            get { return id; }
            set
            {
                try
                {
                    if (value < 0)
                    {
                        throw new IndexOutOfRangeException(nameof(Id));
                    }
                }
                catch (IndexOutOfRangeException idIndexOutOfRangeExceptionn)
                {
                    MessageDialogHelper.Show(
                     "The Event Id cannot be a negative number",
                     "Invalid Event ID!");
                }
                
                
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                try
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ArgumentNullException(nameof(Name));
                    }
                }
                catch (ArgumentNullException nameNullException)
                {

                    MessageDialogHelper.Show(
                     "The Event Name cannot be empty",
                     "Invalid Event Name!");
                }
                
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                try
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ArgumentNullException(nameof(Description));
                    }
                }
                catch (ArgumentNullException descriptionNullException)
                {

                    MessageDialogHelper.Show(
                     "The Event Description cannot be empty",
                     "Invalid Event Description!");
                }
                description = value;
                OnPropertyChanged();
            }
        }

        public string Place
        {
            get { return place; }
            set
            {
                try
                {
                    if (String.IsNullOrEmpty(value))
                    {
                        throw new ArgumentNullException(nameof(Place));
                    }
                }
                catch (ArgumentNullException placeNullException)
                {

                    MessageDialogHelper.Show(
                     "The Event Place cannot be empty",
                     "Invalid Event Place!");
                }
                place = value;
                OnPropertyChanged();
            }
        }
        
        public DateTimeOffset Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }

        public TimeSpan Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(); }
        }

        //Constructor:

        public EventViewModel()
        {
            EventCatalog = EventCatalogSingleton.GetInstance();
            DateTime dt = DateTime.Now;
            Date = new DateTimeOffset(dt);
            Time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            EventHandler = new Handler.EventHandler(this);
            _createEventCommand = new RelayCommand(EventHandler.CreateEvent);
            _deleteEventCommand = new RelayCommand(EventHandler.DeleteEvent);
        }

        //RelayCommand, ICommand

        public ICommand CreateEventCommand
        {
            get
            {
                if (_createEventCommand == null)
                {
                    _createEventCommand = new RelayCommand(EventHandler.CreateEvent);
                }
                return _createEventCommand;
            }
        }

        // TODO part v) in guide:
        // implement this below
        //TODO: what is this? t)+v)+u) in the guide on Gdrive

        public static Event SelectedEvent { get; set; }

        private ICommand _selectedEventCommand;

        public ICommand SelectedEventCommand
        {
            get { return _selectedEventCommand; }
            set { _selectedEventCommand = value; }
        }
        
        private ICommand _deleteEventCommand;

        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand; }
            set { _deleteEventCommand = value; }
        }


        // this is where the magic happens, haven't studied about it yet, it's an "EVENT"
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string header)
            {
                MessageDialog messageDialog = new MessageDialog(content, header);
                await messageDialog.ShowAsync();
            }
        }
    }
}
