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
using Windows.UI;
using Windows.UI.Xaml.Media;
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
        private ICommand _selectedEventCommand;
        private ICommand _deleteEventCommand;
        private SolidColorBrush _textBoxBorderBrushColor;

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
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public string Place
        {
            get { return place; }
            set
            {
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

        public SolidColorBrush TextBoxBorderBrushColor
        {
            get { return _textBoxBorderBrushColor; }
            set
            {
                _textBoxBorderBrushColor = value;
                OnPropertyChanged();
            }
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

        public static Event SelectedEvent { get; set; }
        
        public ICommand SelectedEventCommand
        {
            get { return _selectedEventCommand; }
            set { _selectedEventCommand = value; }
        }
        
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
        
        public bool IsValidEvent()
        {
            bool result = false;

            result = Id != 0;
            result &= !String.IsNullOrEmpty(Name) && !String.IsNullOrWhiteSpace(Name);
            result &= !String.IsNullOrEmpty(Description) && !String.IsNullOrWhiteSpace(Description);
            result &= !String.IsNullOrEmpty(Place) && !String.IsNullOrWhiteSpace(Place);
            
            return result;

        }

        
        //TODO: implement this shit:
        //public void GoBackToEventsAfterSaving()
        //{
        //    INavigate newNavigate = new Frame();
        //    newNavigate.Navigate(typeof(EventPage));
        //}
        
        public void SetBorderBrushColor()
        {
            var green = new SolidColorBrush(Colors.LawnGreen);
            var red = new SolidColorBrush(Colors.Red);
            
            TextBoxBorderBrushColor = IsValidEvent() ? red : green;
            
        }
    }
}
