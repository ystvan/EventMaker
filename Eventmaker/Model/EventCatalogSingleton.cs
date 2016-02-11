using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.Converter;
using Eventmaker.Persistency;
using Eventmaker.ViewModel;

namespace Eventmaker.Model
{
    public class EventCatalogSingleton
    {
        //Singleton 2nd kind of initialization

        private static EventCatalogSingleton _instance = null;

        public static EventCatalogSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventCatalogSingleton();
            }
            return _instance;
        }

        //ObservableCollection to adaptive list(remove, add, load)

        public ObservableCollection<Event> events { get; set; }

        //Singleton constructor is private

        private EventCatalogSingleton()
        {
            events = new ObservableCollection<Event>();
            LoadEventAsync();
        }

        //Just some random events to be shown at the begining

        public async void LoadEventAsync()
        {
            var JsonEvents = await PersistencyService.LoadEventsFromJsonAsync();
            if (JsonEvents != null)
                foreach (var @event in JsonEvents)
                {
                    events.Add(@event);
                }
        }
      
        
        //Below seen some methods to handle the list

        public void AddEvent(int _id, string _name, string _description, string _place, DateTime _dateTime)
        {
            events.Add(new Event(_id, _name, _description, _place, _dateTime));
            PersistencyService.SaveEventsAsJsonAsync(events);
        }

        public void RemoveEvent(Event thisEvent)
        {
            events.Remove(thisEvent);
            PersistencyService.SaveEventsAsJsonAsync(events);
        }

       
    }
}
