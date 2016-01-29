using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker.Converter;
using Eventmaker.ViewModel;

namespace Eventmaker.Model
{
    public class EventCatalogSingleton
    {

        private static EventCatalogSingleton _instance = null;

        public static EventCatalogSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventCatalogSingleton();
            }
            return _instance;
        }

        public ObservableCollection<Event> Events { get; set; }

        private EventCatalogSingleton()
        {
            Events = new ObservableCollection<Event>();
            LoadSomeEvents();
        }

        public void LoadSomeEvents()
        {
            Events.Add(new Event(1, "Team Bulding", "Kick-start 2nd semester by bowling", "Roskilde Bowling Centre", new DateTime(2015,1,29,8,0,0,0)));
            Events.Add(new Event(2, "Group Formation", "Finding New Members", "Classroom E304", new DateTime(2015,1,29,9,0,0,0)));
            Events.Add(new Event(3, "Project Proposals", "Company Visit", "ZIBAT Headquarters", new DateTime(2015,2,1,12,0,0,0)));

        }

        public void AddEvent(int id, string name, string description, string place, DateTime dateTime)
        {
            Events.Add(new Event(id, name, description, place, dateTime));
        }


    }
}
