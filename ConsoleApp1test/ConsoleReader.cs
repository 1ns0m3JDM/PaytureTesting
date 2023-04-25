using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.File;

namespace PaytureTest
{
    public class ConsoleReader
    {
        Validator validator = new Validator();
        public string ReadString(string _name)      
        {
            Console.WriteLine($"Enter {_name}:");

            var _pattern = validator.ValidRegReturn(_name);
            
            var _string = Console.ReadLine();
           
            while (_string == null  || _string.Equals(string.Empty) || !Regex.IsMatch(_string, _pattern))
            {
                Console.WriteLine($"Enter {_name} correctly: ") ;
                _string = Console.ReadLine(); 
            }

            return _string;
        }
    } 
}