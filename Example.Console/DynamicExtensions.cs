using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class DynamicExtensions
    {
        private static IEnumerable<KeyValuePair<string, string>> getPropPairs(object target)
        {
            var lookup = new List<KeyValuePair<string, string>>();
            foreach (var prop in (dynamic)target)
            {
                var value = string.Empty;
                if (((object)prop).GetType().IsAssignableFrom(typeof(string)))
                {
                    value = prop;
                    lookup.Add(new KeyValuePair<string, string>(string.Empty, string.Empty + value));
                }
                else if (value.GetType().IsValueType || ((object)value).GetType().IsAssignableFrom(typeof(string)))
                {
                    var key = prop.Key ?? prop.Name;
                    value = prop.Value + string.Empty;
                    lookup.Add(new KeyValuePair<string, string>(key, value));
                }
                else
                {
                    lookup.AddRange(getPropPairs(value));
                }
            }
            return lookup;
        }

        public static IEnumerable<KeyValuePair<string, string>> ToDynamicLookup(this object target)
        {
            return getPropPairs(target);
        }
    }
}
