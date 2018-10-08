using System;
using System.Text.RegularExpressions;

namespace Monitor.Validation
{
    public class PasswordValidation<T>: IValidationRule<T>
    {
        public string ValidationMessage { get; set; }  

        public bool Check(T value)  
        {  
            if (value == null)  
            {  
                return false;  
            }  

            var str = value as string;  
            Regex regex = new Regex( @"^(?=[^\d_].*?\d)\w(\w|[!@#$%]){7,20}");
            Match match = regex.Match(str);  

            return match.Success;  
        }  
    }
}
