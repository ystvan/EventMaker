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
            LoadSomeEvents();
        }

        //Just some random events to be shown at the begining

        public void LoadSomeEvents()
        {
            //TODO change the method part q) in the guideline public >>>async void LoadEventAsync(){} await LoadJson

            events.Add(new Event(1, "Team Bulding", "Kick-start 2nd semester by bowling", "Roskilde Bowling Centre", new DateTime(2015,1,29,8,0,0,0)));
            events.Add(new Event(2, "Group Formation", "Finding New Members", "Classroom E304", new DateTime(2015,1,29,9,0,0,0)));
            events.Add(new Event(3, "Project Proposals", "Company Visit", "ZIBAT Headquarters", new DateTime(2015,2,1,12,0,0,0)));

        }

        //Below seen some methods to handle the list

        public void AddEvent(int _id, string _name, string _description, string _place, DateTime _dateTime)
        {
            events.Add(new Event(_id, _name, _description, _place, _dateTime));
        }

        public void RemoveEvent()
        {
           //TODO: implement this r)
        }

       
    }
}
