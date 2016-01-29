using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Eventmaker.Model;

namespace Eventmaker.Persistency
{
    class PersistencyService
    {
        public static async void SaveEventsAsJsonAsync(ObservableCollection<Event> Events)
        {
            StorageFolder
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {

        }

        public static async void SerializeEventsFileAsync(string eventsString, string fileName)
        {

        }

        public static async Task<string> DeSerializeEventsFileAsync(String fileName)
        {

        }
    }
}
