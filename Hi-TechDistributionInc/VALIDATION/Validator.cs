using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hi_TechDistributionInc.VALIDATION
{
    public static class Validator
    {
        public static bool IsvalidEmail(string input)
        {
            if (!(Regex.IsMatch(input, @"^([a-zA-Z\.\d]+)@([a-zA-Z\d-]+)\.([a-zA-Z]{2,8})(\.[a-zA-Z]{2.8})?$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidNum(string input, int size)
        {
            if (!(Regex.IsMatch(input, @"^\d{" + size + "}$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidString(string input, int minSize, int maxSize)
        {
            if (!(Regex.IsMatch(input, @"^[a-zA-Z]{"+ minSize + "," + maxSize + "}?$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidAddress(string input)
        {
            if (!(Regex.IsMatch(input, @"^\d{1,8}\s[a-zA-Z]{2,20}(\s[a-zA-Z]{3,20})?$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidPostalCode(string input)
        {
            if (!(Regex.IsMatch(input, @"^[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidNum2(string input, int minSize, int maxSize)
        {
            if (!(Regex.IsMatch(input, @"^\d{"+ minSize + ", "+ maxSize + "}$")))
            {
                return false;
            }
            return true;
        }
        public static bool IsvalidWebSite(string input)
        {
            if (!(Regex.IsMatch(input, @"^([a-zA-Z\d]+)\.([a-zA-Z]{2,8})?$")))
            {
                return false;
            }
            return true;
        }
    }
}
