using JsonFlatFileDataStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace hroabot.Classes
{
    public class speaker
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("intros")]
        public List<string> Intros { get; set; } = new List<string>();

        [JsonProperty("outros")]
        public List<string> Outros { get; set; } = new List<string>();

        public static speaker[] FromJson(string json) => JsonConvert.DeserializeObject<speaker[]>(json, Converter.Settings);

        public static void add_speaker(speaker speaker)
        {
            var store = new DataStore("speaker.json");

            // Get employee collection
            store.GetCollection<speaker>().InsertOneAsync(speaker);

            store.Dispose();
        }

        public static speaker get_speaker(int id)
        {
            var store = new DataStore("speaker.json");
            
            speaker toReturn = store.GetCollection<speaker>().AsQueryable().FirstOrDefault(e => e.ID == id);
            store.Dispose();
            return toReturn;
        }

        public static void update_speaker(speaker speaker)
        {
            var store = new DataStore("speaker.json");

            store.GetCollection<speaker>().ReplaceOneAsync(e => e.ID == speaker.ID, speaker);
            store.Dispose();
        }
    }
}
