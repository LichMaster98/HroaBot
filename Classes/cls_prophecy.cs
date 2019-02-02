using JsonFlatFileDataStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace hroabot.Classes
{
    public class prophecy
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        public static prophecy[] FromJson(string json) => JsonConvert.DeserializeObject<prophecy[]>(json, Converter.Settings);

        public static prophecy get_prophecy(string stringId)
        {
            if (int.TryParse(stringId, out int id))
            {
                var store = new DataStore("prophecy.json");

                prophecy toReturn = store.GetCollection<prophecy>().AsQueryable().FirstOrDefault(e => e.ID == id);

                store.Dispose();

                return toReturn;
            }
            else
            {
                return null;
            }
        }

        public static void add_prophecy(prophecy prop)
        {
            var store = new DataStore("prophecy.json");

            // Get employee collection
            store.GetCollection<prophecy>().InsertOneAsync(prop);

            store.Dispose();
        }
    }
}
