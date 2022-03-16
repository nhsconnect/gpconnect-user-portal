using Newtonsoft.Json;
using System.Collections.Generic;

namespace gpconnect_user_portal.Helpers
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }
    }
}
