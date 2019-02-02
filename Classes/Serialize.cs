using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace hroabot.Classes
{
    public static class Serialize
    {
        public static string ToJson(this description[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this speaker[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
        public static string ToJson(this prophecy[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
