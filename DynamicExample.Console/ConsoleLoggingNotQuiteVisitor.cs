using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ConsoleLoggingNotQuiteVisitor
    {
        public void Output(IEnumerable<KeyValuePair<string, dynamic>> lookup)
        {
            foreach (var pair in lookup)
            {
                Output(pair.Key, pair.Value);
            }
        }
        private void Output(IEnumerable<object> values)
        {
            foreach (var value in values)
            {
                Output( (dynamic)value);
            }
        }

        private void Output(IConvertible value)
        {
            Console.WriteLine(value);
        }

        private void Output(string name, IConvertible value)
        {
            Console.WriteLine(name);
            Console.WriteLine(value);
        }

        private void Output(string name, dynamic value)
        {
            Console.Write(name);
            Output(value);
        }
    }
}
