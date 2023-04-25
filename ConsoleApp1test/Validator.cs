using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaytureTest
{
    public class Validator
    {
        public static string ValidRegReturn(string name)
        {
           string regex = String.Empty;

            switch (name)
            {
                case "Amount":
                    regex = @"^([1-9]\d*)$";
                    break;

                case "PAN":
                    regex = @"^[2-5]\d{3}\d{4}\d{4}\d{4}$";
                    break;

                case "EMonth":
                    regex = @"^(0?[1-9]|1[0-2])$";
                    break;

                case "EYear":
                    regex = @"^\d{2}$";
                    break;

                case "CardHolder":
                    regex = @"^[A-Za-z]+ [A-Za-z]+$";
                    break;

                case "SecureCode":
                    regex = @"^\d{3,4}$";
                    break;
            }
            return regex;
        }
    }
}


