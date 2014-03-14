using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            var factory = new LooseObjectFactory();
            var visitor = new LookupBuildingNotQuiteVisitor();
            var visitorDynamic = new ConsoleLoggingNotQuiteVisitor();

            // anonymous type, serialized and deserialized
            var anonJsonResult = factory.GetAnonymousTypeJsonResult().Data;
            // show prop names 
            OutputNames(anonJsonResult.GetMemberNames());
            //output jproperty / ienumerable<string, JObject>
            visitor.AcceptNoRecursion(anonJsonResult);
            // read properties recursively - building lookup<string, object>
            var lookup = anonJsonResult.ToDynamicLookup();
            // output the lookup recursively
            visitorDynamic.Output(lookup);

            // text, parsed and deserialized
            var parsedJObject = factory.GetParsedJObject();
            // show prop names 
            OutputNames(parsedJObject.GetMemberNames());
            //output jproperty / ienumerable<string, JObject>
            visitor.AcceptNoRecursion(parsedJObject);
            // read properties recursively - building lookup<string, object>
            lookup = parsedJObject.ToDynamicLookup();
            // output the lookup recursively
            visitorDynamic.Output(lookup);

            // text, parsed, deserialized in JsonObject.Data
            var parsedJsonResult = factory.GetParsedJsonResult().Data;
            // show prop names 
            OutputNames(parsedJsonResult.GetMemberNames());
            //output jproperty / ienumerable<string, JObject>
            visitor.AcceptNoRecursion(parsedJsonResult);
            // read properties recursively - building lookup<string, object>
            lookup = parsedJsonResult.ToDynamicLookup();
            // output the lookup recursively
            visitorDynamic.Output(lookup);
        }

        private static void OutputNames(IEnumerable<string> names)
        {
            Console.WriteLine("Member Names:");
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }







    }
}
