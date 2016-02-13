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
        private SolidColorBrush _idTextBoxBorderBrushColor;
        private SolidColorBrush _nameTextBoxBorderBrushColor;
        private SolidColorBrush _descriptionTextBoxBorderBrushColor;
        private SolidColorBrush _placeTextBoxBorderBrushColor;

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

        public SolidColorBrush IdTextBoxBorderBrushColor
        {
            get { return _idTextBoxBorderBrushColor; }
            set
            {
                _idTextBoxBorderBrushColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush NameTextBoxBorderBrushColor
        {
            get { return _nameTextBoxBorderBrushColor; }
            set
            {
                _nameTextBoxBorderBrushColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush DescriptionTextBoxBorderBrushColor
        {
            get { return _descriptionTextBoxBorderBrushColor; }
            set
            {
                _descriptionTextBoxBorderBrushColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush PlaceTextBoxBorderBrushColor
        {
            get { return _placeTextBoxBorderBrushColor; }
            set
            {
                _placeTextBoxBorderBrushColor = value;
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
        
        // validating the Event, checking for missing fields
        public bool IsValidEvent()
        {
            bool result = false;

            result = Id != 0;
            result &= !String.IsNullOrEmpty(Name) && !String.IsNullOrWhiteSpace(Name);
            result &= !String.IsNullOrEmpty(Description) && !String.IsNullOrWhiteSpace(Description);
            result &= !String.IsNullOrEmpty(Place) && !String.IsNullOrWhiteSpace(Place);
            
            return result;

        }
        
        // setting the textboxes' bordercolor, that is being binded in the view
        public void SetBorderBrushColor()
        {
            var green = new SolidColorBrush(Colors.LawnGreen);
            var red = new SolidColorBrush(Colors.Red);
            
            IdTextBoxBorderBrushColor = Id <= 0 ? red : green;

            if (String.IsNullOrEmpty(Name) && String.IsNullOrWhiteSpace(Name))
            {
                NameTextBoxBorderBrushColor = red;
            }
            else
            {
                NameTextBoxBorderBrushColor = green;
            }
            if (String.IsNullOrEmpty(Description) && String.IsNullOrWhiteSpace(Description))
            {
                DescriptionTextBoxBorderBrushColor = red;
            }
            else
            {
                DescriptionTextBoxBorderBrushColor = green;
            }
            if (String.IsNullOrEmpty(Place) && String.IsNullOrWhiteSpace(Place))
            {
                PlaceTextBoxBorderBrushColor = red;
            }
            else
            {
                PlaceTextBoxBorderBrushColor = green;
            }
                
            
        }

    }
}
