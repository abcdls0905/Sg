using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlatform
{

    public interface IBigDataBuilder
    {
        void PushData(string data);
        void AddRawData(string data);

        void AddPairData(string key, string value);
        void Clear();
        string String();
    }

    class BigDataBuilder :  IBigDataBuilder
    {
        public BigDataBuilder(string sep)
        {
            builder = new StringBuilder();
            SingleSep = sep;
        }
        public BigDataBuilder()
        {
            builder = new StringBuilder();
            SingleSep = "\t";
            MultSep = ",";
            PairSep = ":";
            contextSep = new string[] { "&","_", "|" };
        }

        public void SetSingleSep(string sep)
        {
            SingleSep = sep;
        }

        public void SetMultSep(string sep)
        {
            MultSep = sep;
        }

        public void SetPairSep(string sep)
        {
            PairSep = sep;
        }

        public void PushData(string data)
        {
            if (builder.Length != 0) {
                builder.Append(SingleSep);
            }
            builder.Append(data);
        }

        public void AddPairData(string key, string value)
        {
            if (builder.Length  != 0)
            {
                builder.Append(MultSep);
            }
            builder.Append(key);
            builder.Append(PairSep);
            builder.Append(value);
        }


        public void AddRawData(string data)
        {
            builder.Append(data);
        }

        public void Clear()
        {
            builder = new StringBuilder();
        }
        public string String() { return builder.ToString(); }

        private string SingleSep;
        private string MultSep;
        private string PairSep;

        public string[] contextSep;

        private StringBuilder builder;
    }
}
