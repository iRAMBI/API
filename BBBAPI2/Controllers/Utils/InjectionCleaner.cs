using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BBBAPI2.Controllers.Utils
{
    public class InjectionCleaner
    {
        public static string cleanString(String data)
        {
            Debug.WriteLine("Incoming String: " + data);
            String filtered = data.Replace("'", "\\'");
            Debug.WriteLine("Outgoing String: " + filtered);
            return filtered;
        }
    }
}