using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobolRecordParser
{
    class Picture
    {
        public string RawValue { get; private set; }
        public double Size { get; private set; }
        public PicType Type { get; private set; }

        public Picture()
        {
            RawValue = null;
            Size = 0;
            Type = PicType.unknown;
        }

        public Picture(string sizeString)
        {
            RawValue = sizeString;
            extractPicSize();
            extractType();

        }

        private void extractType()
        {
            Type = PicType.integer;
        }

        /// <summary>
        /// Uses the set RawValue to work out the size of the picture field as a double.
        /// </summary>
        private void extractPicSize()
        {
            if (string.IsNullOrWhiteSpace(RawValue))
            {
                Size = 0;
                return;
            }

            string value = RawValue.replaceVs();
            // we're going to need to parse this string somehow.
            value = value.stripTypeDefs();

            Console.WriteLine(value);

            Size = double.Parse(value);
        }
    }
}
