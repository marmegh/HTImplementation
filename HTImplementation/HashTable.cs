using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTImplementation
{
    public class HashTable
    {
        public int count { get; set; }
        private List<kvpair>[] DATA { get; set; }
        private List<kvpair>[] nuDATA { get; set; }
        public struct kvpair
        {
            public string key;
            public int val { get; set; }
            public bool flag;
            public kvpair(string keys, int value)
            {
                key = keys;
                val = value;
                flag = false;
            }
            public void kvupdate(int value)
            {
                val = value;
            }
        }
        public HashTable(int length)
        {
            DATA = new List<kvpair>[length];
            int[] thingy = new int[5];
        }
        private float loadfactor()
        {
            return count / DATA.Length;
        }
        private void resize()
        {
            nuDATA = new List<kvpair>[DATA.Length * 2];
            foreach(List<kvpair> item in DATA)
            {
                foreach(kvpair pair in item)
                {
                    Modify(pair.key, pair.val, nuDATA);
                }
            }
            DATA = nuDATA;
        }
        private int _hash(string key)
        {
            int chartotal = 0;
            foreach(char thing in key)
            {
                chartotal += System.Convert.ToInt32(thing);
            }
            return chartotal % DATA.Length;
        }        
        private void Modify(string nukey, int value, List<kvpair>[] datum)
        {
            if(loadfactor() > 3 && datum == DATA)
            {
                resize();
            }
            int hashed = _hash(nukey);
            if(datum[hashed].Count > 0)
            {
                for(int i = 0; i<datum[hashed].Count; i++)
                {
                    if (datum[hashed][i].key == nukey)
                    {
                        datum[hashed][i].kvupdate(value);
                        count++;
                        return;
                    }
                }
            }
            datum[hashed].Add(new kvpair(nukey, value));
            count++;
        }
        public void Add(string nukey, int value)
        {
            Modify(nukey, value, DATA);
        }

        public int? Get(string olkey)
        {
            int hashed = _hash(olkey);
            if(DATA[hashed].Count > 0)
            {
                foreach(kvpair pair in DATA[hashed])
                {
                    if (pair.key == olkey)
                    {
                        return pair.val;
                    }
                }
            }
            return null;
        }
        public int? Remove(string olkey)
        {
            int hashed = _hash(olkey);            
            if (DATA[hashed].Count > 0)
            {
                for(int i = 0; i < DATA[hashed].Count; i++)
                {
                    if (DATA[hashed][i].key == olkey)
                    {
                        kvpair result = DATA[hashed][i];
                        DATA[hashed].Remove(result);
                        return result.val;
                    }
                }
            }
            return null;

        }
    }
}
