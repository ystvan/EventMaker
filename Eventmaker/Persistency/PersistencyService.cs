using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Eventmaker.Model;
using Newtonsoft.Json;
using Windows.UI.Popups;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Eventmaker.Persistency
{
    class PersistencyService
    {
        private static string jsonFileName = "EventsAsJson1.dat";
        
        public static async void SaveEventsAsJsonAsync(ObservableCollection<Event> events)
        {
            string eventsJsonString = JsonConvert.SerializeObject(events);
            SerializeEventsFileAsync(eventsJsonString, jsonFileName);
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
            string eventsJsonString = await DeSerializeEventsFileAsync(jsonFileName);
            if (eventsJsonString != null)
            {
                return (List<Event>) JsonConvert.DeserializeObject(eventsJsonString, typeof (List<Event>));
            }
            return null;
        }

        public static async void SerializeEventsFileAsync(string eventsString, string fileName)
        {
            StorageFile localFile =
                await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                        CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, eventsString);
        }

        public static async Task<string> DeSerializeEventsFileAsync(String fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException exepction)
            {

                MessageDialogHelper.Show(
                    "Your Event List is empty! First try adding and save some Events before you Load Events!",
                    "No Events to show!");
                return null;
            }
        }

        private class MessageDialogHelper
        {
            public static async void Show(string content, string header)
            {
                MessageDialog messageDialog = new MessageDialog(content, header);
                await messageDialog.ShowAsync();
            }
        }

        //Not sure if we need to save it to xml format but this can be useful anyway later on
        // Just uncomment the code below:
        //
        //private static string xmlFileName = "EventsAsXml1.dat";

        //public static async void SaveEventsAsXmlAsync(ObservableCollection<Event> events)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(events.GetType());
        //    StringWriter textWriter = new StringWriter();
        //    xmlSerializer.Serialize(textWriter, events);
        //    SerializeEventsFileAsync(textWriter.ToString(), xmlFileName);
        //}

        //
    }
}
