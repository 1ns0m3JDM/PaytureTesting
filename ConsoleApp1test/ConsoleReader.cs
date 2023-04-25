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
        public string ReadString(string name)      
        {
            Console.WriteLine($"Enter {name}:");

            var pattern = Validator.ValidRegReturn(name);
            
            var rl = Console.ReadLine();

            while (String.IsNullOrEmpty(rl) || !Regex.IsMatch(rl, pattern))
            {
                Console.WriteLine($"Enter {name} correctly: ") ;
                rl = Console.ReadLine(); 
            }

            return rl;
        }
    } 
}