using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Code_Generator
{
    public class clsValidatoin
    {

        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(emailAddress);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }


        public static bool ValidateProperties(string Properties)
        {
            //example: ID:int,Name:string,Email:string
            //var pattern = @"^[A-Za-z0-9_]*ID[A-Za-z0-9_]*:(int|string|bool|byte|DateTime|double|decimal)(,\s*\w+:(int|string|bool|byte|DateTime|double|decimal))*$";
            var pattern = @"^(?!.*\b(\w+):\w+.*,\s*\1:\w+)[A-Za-z0-9_]*ID[A-Za-z0-9_]*:(int|string|char|bool|byte|DateTime|double|float|decimal)(,\s*\w+:(int|string|char|bool|byte|DateTime|double|float|decimal))*$";
            
            var regex = new Regex(pattern);

            return regex.IsMatch(Properties);
        }



    }
}
