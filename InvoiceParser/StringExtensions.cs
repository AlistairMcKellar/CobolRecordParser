using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobolRecordParser
{
    static class stringExtensions
    {
        public static string replaceVs(this string value)
        {
            return value.Replace("v", ".").Replace("V", ".").Replace("+", "-").Replace("s", "-"); ;
        }

        public static string stripTypeDefs(this string value)
        {
            string[] test = new string[] {
                            "9(",
                            "x(",
                            "X(",
                            "z(",
                            "Z("};

            for (int i = 0; i < test.Length; i++)
            {
                while (value.Contains(test[i]))
                {
                    value = value.Remove(value.IndexOf(test[i]), 2);
                }
            }

            while (value.Contains(")"))
            {
                value = value.Remove(value.IndexOf(")"), 1);
            }

            return value;
        }
    }
}
