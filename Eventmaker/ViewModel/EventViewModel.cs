using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Eventmaker.Annotations;
using Eventmaker.Common;
using Eventmaker.Converter;
using Eventmaker.Model;
using Eventmaker.View;

namespace Eventmaker.ViewModel
{


    class EventViewModel : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string description;
        private string place;
        private DateTimeOffset date;
        private TimeSpan time;
        private ICommand _createEventCommand;
        public EventCatalogSingleton EventCatalog { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        public string Place
        {
            get { return place; }
            set { place = value; OnPropertyChanged(); }
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



        

        public EventViewModel()
        {
            EventCatalog = EventCatalogSingleton.GetInstance();
            DateTime dt = DateTime.Now;
            Date = new DateTimeOffset(dt);
            Time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
        }

        public ICommand CreateEventCommand
        {
            get
            {
                if (_createEventCommand == null)
                {
                    _createEventCommand = new RelayCommand(CreateEvent);
                }
                return _createEventCommand;
            }
        }

        public ObservableCollection<Event> Events
        {
            get { return EventCatalog.Events; }

            set { EventCatalog.Events = value; }
        }
        

        public void CreateEvent()
        {
            Events.Add(new Event(Id, Name, Description, Place, DateTimeConverter.DateTimeOffsetAndDateTime(Date, Time)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
