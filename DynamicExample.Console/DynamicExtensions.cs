using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    public static class DynamicExtensions
    {
        private static IEnumerable<KeyValuePair<string, object>> propPairs(IEnumerable<object> target)
        {
            return target.Select(element => 
                new KeyValuePair<string, object>(string.Empty, element + string.Empty)).ToList();
        }

        private static IEnumerable<KeyValuePair<string, object>> propPairs(string name, IEnumerable<object> target)
        {
            var targetValues = (from dynamic value in target select prop(value)).ToList();

            return new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(name, targetValues)
            };
        }

        private static IEnumerable<KeyValuePair<string, object>> propPairs(KeyValuePair<string, object> target)
        {
            dynamic value = target.Value;
            return propPairs(target.Key, value);
        }

        private static IEnumerable<KeyValuePair<string, object>> propPairs(string name, DynamicJsonObject target)
        {
            var value = target.ToDynamicLookup();
            return new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>(name, value)
            };
        }

        private static dynamic prop(DynamicJsonObject json)
        {
            return json.ToDynamicLookup();
        }

        private static dynamic prop(IConvertible value)
        {
            return value.ToString();
        }

        private static IEnumerable<KeyValuePair<string, object>> propPairs(string name,  IConvertible target)
        {
            return new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>(name, target) };
        }


        private static IEnumerable<KeyValuePair<string, object>> GetPropPairs(dynamic target)
        {
            var lookup = new List<KeyValuePair<string, object>>();
            foreach (var prop in (dynamic)target)
            {
                lookup.AddRange(propPairs(prop));
            }
            return lookup;
        }

        public static IEnumerable<string> GetMemberNames(this object target)
        {
            var metaObjectProvider = target as IDynamicMetaObjectProvider;
            if (metaObjectProvider != null)
            {
                var meta = metaObjectProvider.GetMetaObject(Expression.Constant(target));
                return meta.GetDynamicMemberNames();
            }

            var dynamicType = target.GetType();
            return dynamicType.GetProperties().Select(p => p.Name).ToList();
        }



        public static IEnumerable<KeyValuePair<string, object>> ToDynamicLookup(this object target)
        {
            dynamic dynamicTarget = target;
            return GetPropPairs(dynamicTarget);
        }


    }
}
