using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace ConsoleApplication1
{
    class Program
    {
        static readonly string totallyRandomObject = @"{
        'id': 0,
        'guid': 'aee91380-8617-4c0e-9f11-d56448424d7a',
        'isActive': true,
        'balance': '$1,893.00',
        'picture': 'http://placehold.it/32x32',
        'age': 31,
        'name': 'Moon Ruiz',
        'gender': 'male',
        'company': 'Jasper',
        'email': 'moonruiz@jasper.com',
        'phone': '+1 (911) 562-3576',
        'address': '807 Quentin Street, Florence, Idaho, 1845',
        'about': 'Eu reprehenderit Lorem sunt ad reprehenderit. Elit esse excepteur non consectetur consectetur fugiat labore fugiat esse culpa sint consequat aute. Adipisicing velit aliqua tempor nulla. Ad exercitation officia eu cupidatat culpa ea. Irure velit Lorem nulla aliquip exercitation anim proident non ut et adipisicing consequat.\r\n',
        'registered': '2012-08-16T21:59:03 +05:00',
        'latitude': -41.380898,
        'longitude': 80.326358,
        'tags': [
            'ipsum',
            'cillum',
            'voluptate',
            'elit',
            'incididunt',
            'ut',
            'officia'
        ],
        'friends': [
            {
                'id': 0,
                'name': 'Lara Carlson'
            },
            {
                'id': 1,
                'name': 'Spears Bryant'
            },
            {
                'id': 2,
                'name': 'Jana Whitaker'
            }
        ],
        'customField': 'Hello, Moon Ruiz! You have 7 unread messages.'
    }";
        static void Main(string[] args)
        {
            var result = new JsonResult();
            result.Data = Json.Decode(totallyRandomObject);
            EnumerateProperties(result.Data);

            var lookup = result.Data.ToDynamicLookup();
            foreach (var pair in lookup)
            {
                Console.WriteLine(pair.Key);
                Console.WriteLine(pair.Value);
            }

            var jobject = JObject.Parse(totallyRandomObject);
            EnumerateProperties(jobject);

        }

        private static void EnumerateProperties(object result)
        {
            dynamic resultDynamic = result;
            IDynamicMetaObjectProvider meta = resultDynamic as IDynamicMetaObjectProvider;
            var nameProp = meta.GetMetaObject(DynamicExpression.Property()

            foreach (var prop in resultDynamic)
            {
                Console.WriteLine(prop.Name ?? prop.Key);
                Console.WriteLine(prop.Value);
            }
        }


    }
}
