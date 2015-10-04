using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CobolRecordParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoiceStream = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            string invoice;
            using (var invoiceReader = new StreamReader(invoiceStream))
            {
                invoice = invoiceReader.ReadToEnd();
            }

            var parser = new Regex(@"(?imnx)^                           # A regular expression to parse line item - set to ignore case, is multiline, explicit capture, allows white space
                                    \s*                                 # starting spaces
                                    \d{2}                               # 2 digits
                                    \s+                                 # more spaces
                                    \(                                  # open bracket
                                    \w+                                 # field prefix
                                    \)                                  # close bracket
                                    -                                   # Match up beyond the first - ie (MEA0)-
                                    (?<name>(\w|-)+)                    # name of the field
                                    \s+                                 # Skip over the white space
                                    (pic|PIC)\s+                        # and the pic clause
                                    (?<size>
                                        ((s|-|\+)*                      # signed value?
                                            (?<type>(X|9|z){1})         # capture the type - must start with a 9 x or z
                                            \(*                         # if set a open ( will be captured
                                            (z|x|\d)+                   # will capture any numbers or a z or x if doing a PIC xx
                                            \)*                         # capture any close brackets
                                            (\.|v)*                     # capture a decimal declaration
                                            (z|x|9)*                    # initial z x or 9 again
                                            \(*                         # open bracket?
                                            (Z|\d)*                     # more values allowed for the decimal places
                                            (z|x|9|\)){1}               # must end in either an X z 9 or )
                                        )
                                    )
                                    \s*\.\s*$");
            Console.WriteLine(invoice);
            var match = parser.Match(invoice);
            while (match.Success)
            {

                Console.WriteLine("name:  {0}", match.Groups["name"].Value);
                Console.WriteLine(" Type: {0}", match.Groups["type"].Value);
                Record newRecord = new Record(match.Groups["name"].Value, match.Groups["size"].Value);
                Console.WriteLine(" size: {0}", match.Groups["size"].Value);    // need a helper method to strip out the size
                Console.WriteLine("   post processing size: {0}", newRecord.size());

                //foreach (Group group in match.Groups)
                //{
                //    foreach (var capture in match.Captures)
                //    {
                //        Console.WriteLine(capture);
                //    }
                //}
                match = match.NextMatch();
            }
            Console.WriteLine("end of file");
        }


    }
}
