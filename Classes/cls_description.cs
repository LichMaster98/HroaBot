using System.Collections.Generic;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JsonFlatFileDataStore;
using System.Linq;
using hroabot.Classes;

namespace hroabot.Classes {

    public partial class description {
        [JsonProperty ("ID")]
        public int ID { get; set; }

        [JsonProperty ("title")]
        public string title { get; set; }

        [JsonProperty ("author")]
        public ulong author { get; set;}

        [JsonProperty ("description")]
        public string descr { get; set; }

        [JsonProperty ("img")]
        public string img { get; set; } = "";
        [JsonProperty("rgb")]
        public int[] rgb { get; set; } = { 255, 255, 255};

        public Embed toEmbed(SocketGuild Guild) {
            var embed = new EmbedBuilder();

            embed.Title = title;
            if (img != "") {
                embed.WithImageUrl(img);
            }
            embed.AddField("Description", descr, true);
            embed.AddField("Author", Guild.GetUser(author).Mention);
            embed.WithColor(rgb[0], rgb[1], rgb[2]);
            embed.Build();

            return embed.Build();
        }

        public static bool isDescriptionManager(SocketGuildUser User) {
            if (User.Id == 106768024857501696) return true; //Giving Xavier Automatic access to description access
            var roles = User.Roles;
            var manager = roles.FirstOrDefault(e=>e.Name == "Description"); //Check for 'Description' roll
            if (manager != null) return true;
            else {
                manager = roles.FirstOrDefault(e=>e.Permissions.Administrator);
                return (manager != null);
            }
        }
    }
    public partial class description
    {
        public static description[] FromJson(string json) => JsonConvert.DeserializeObject<description[]>(json, Converter.Settings);

        

        public static List<description> get_description () {
            var store = new DataStore ("description.json");

            // Get employee collection
            var rtrner = store.GetCollection<description> ().AsQueryable ().ToList();
            store.Dispose();
            return rtrner;
        }

        public static description get_description (int id) {
            var store = new DataStore ("description.json");

            // Get employee collection
            var rtrner = store.GetCollection<description> ().AsQueryable ().FirstOrDefault (e => e.ID == id);
            store.Dispose();
            return rtrner;
        }

        public static description get_description (string name) {
            var store = new DataStore ("description.json");

            // Get employee collection
            var rtrner = store.GetCollection<description> ().AsQueryable ().FirstOrDefault (e => e.title == name);
            store.Dispose();
            return rtrner;
        }

        public static void insert_description (description description) {
            var store = new DataStore ("description.json");

            // Get employee collection
            store.GetCollection<description> ().InsertOneAsync (description);

            store.Dispose();
        }

        public static void update_description (description description) {
            var store = new DataStore ("description.json");

            store.GetCollection<description> ().ReplaceOneAsync (e => e.ID == description.ID, description);
            store.Dispose();
        }

        public static void delete_description (description description) {
            var store = new DataStore ("description.json");

            store.GetCollection<description> ().DeleteOne (e => e.ID == description.ID);
            store.Dispose();
        }
    }

    public static class Serialize
    {
        public static string ToJson(this description[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}