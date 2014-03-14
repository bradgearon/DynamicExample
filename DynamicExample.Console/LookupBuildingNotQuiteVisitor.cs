using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    public class LookupBuildingNotQuiteVisitor
    {
        public void AcceptNoRecursion(object element)
        {
            dynamic resultDynamic = element;
            foreach (var prop in resultDynamic)
            {
                Visit(prop);
            }   
        }

        private void Visit(JProperty prop)
        {
            Console.WriteLine(prop.Name);
            Console.WriteLine(prop.Value);
        }

        private void Visit(KeyValuePair<string, object> prop)
        {
            Console.WriteLine(prop.Key);
            Console.WriteLine(prop.Value);
        }

    }
}
