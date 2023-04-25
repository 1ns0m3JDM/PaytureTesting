using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaytureTest
{
    public class Validator
    {
        public string ValidRegReturn(string _string)
        {
            string _regex = "";

            switch (_string)
            {
                case "Amount":
                    _regex = @"^([1-9]\d*)$";
                    break;

                case "PAN":
                    _regex = @"^[2-5]\d{3}\d{4}\d{4}\d{4}$";
                    break;

                case "EMonth":
                    _regex = @"^(0?[1-9]|1[0-2])$";
                    break;

                case "EYear":
                    _regex = @"^\d{2}$";
                    break;

                case "CardHolder":
                    _regex = @"^[A-Za-z]+ [A-Za-z]+$";
                    break;

                case "SecureCode":
                    _regex = @"^\d{3,4}$";
                    break;
            }
            return _regex;
        }
    }
}
         

