using System;
using System.Text.RegularExpressions;

namespace Monitor.Validation
{
	public class URLRule<T>: IValidationRule<T>
    {
		public string ValidationMessage { get; set; }  

        public bool Check(T value)  
        {  
            if (value == null)  
            {  
                return false;  
            }  

            var str = value as string;  
            Regex regex = new Regex(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$");
            Match match = regex.Match(str);  

            return match.Success;  
        }  
    }
}
