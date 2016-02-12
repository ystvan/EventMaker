using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace Eventmaker.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<object> ObservableCollection
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Event() 
        {
            
        }

        public Event(int id, string name, string description, string place, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Description = description;
            Place = place;
            DateTime = dateTime;


        }

        public override string ToString()
        {
            return  String.Format("Id:{0}, Name:{1}, Description:{2}, Place:{3}, DateTime:{4}", Id, Name, Description, Place, DateTime);
        }

        

        

    }
}
