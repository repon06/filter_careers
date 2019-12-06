using System;
using System.Text.RegularExpressions;

namespace veeam_ui_test.Utils
{
    public class StringHelper
    {
        public static int getFirstNumber(string text)
        {
            string pattern = @"\d.";
            var rg = new Regex(pattern);
            var matches = rg.Matches(text);
            return matches.Count > 0 ? Convert.ToInt16(matches[0].ToString()) : 0;
        }
    }
}