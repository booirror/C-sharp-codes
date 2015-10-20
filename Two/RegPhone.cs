using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegularExpressions
{
    class RegPhone
    {
        public static void Test()
        {
            string[] sa = {
                              "Dr. David Jones, Ophthalmology, x2441",
                              "Ms. Cind_y Harriman, Registry, x6231",
                              "Mr. Chester ^Addams, Mortuay, x1668",
                              "Dr. Hawkeye Pierce, Surgery, x0986",
                          };
            string pattern = @"^(?<name>[\. a-zA-z]+), [a-zA-z]+, x(?<ext>\d+)$";
            Regex rx = new Regex(pattern);
            foreach(string s in sa)
            {
                Match m = rx.Match(s);
                if (m.Success)
                {
                    Console.Write(m.Result("${ext}, ${name}"));
                    Console.WriteLine("\t" + rx.Replace(s, "姓: ${name}, 分机号: ${ext}"));
                }
            }
        }
    }
}
