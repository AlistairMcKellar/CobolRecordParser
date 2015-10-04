using System;

namespace CobolRecordParser
{
    internal class Record
    {
        public string Name { get; private set; }
        private Picture picture;

        public Record()
        {
            Name = null;
            picture = null;
        }

        public Record(string name, string size)
        {
            this.Name = name;
            picture = new Picture(size);
        }

        internal double size()
        {
            return picture.Size;
        }
    }
}