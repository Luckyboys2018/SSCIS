using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSCISTest.Class.TestObjects
{
    public class HttpSessionStateBaseTO : HttpSessionStateBase
    {

        private Dictionary<object, object> _dictionary;

        public HttpSessionStateBaseTO()
        {
            _dictionary = new Dictionary<object, object>();
        }
        public bool ContainsKey(object key)
        {
            foreach (var k in Keys)
            {
                if (k.Equals(key)) return true;
            }
            return false;
        }

        public object this[object key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        public override Object this[string key]
        {
            get { return _dictionary[key]; }
            set { this._dictionary[key] = value; }
        }

        public override void Remove(string name)
        {
            _dictionary.Remove(name);
        }

        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                NameObjectCollectionBase.KeysCollection keys = new NameObjectCollectionTO(_dictionary, true).Keys;
                return keys;
            }
        }

    }

    public class NameObjectCollectionTO : NameObjectCollectionBase
    {

        // Adds elements from an IDictionary into the new collection.
        public NameObjectCollectionTO(IDictionary d, Boolean bReadOnly)
        {
            foreach (DictionaryEntry de in d)
            {
                this.BaseAdd((String)de.Key, de.Value);
            }
            this.IsReadOnly = bReadOnly;
        }

        // Gets a key-and-value pair (DictionaryEntry) using an index.
        public DictionaryEntry this[int index]
        {
            get
            {
                return (new DictionaryEntry(
                    this.BaseGetKey(index), this.BaseGet(index)));
            }
        }

        // Gets or sets the value associated with the specified key.
        public Object this[String key]
        {
            get
            {
                return (this.BaseGet(key));
            }
            set
            {
                this.BaseSet(key, value);
            }
        }

        // Gets a String array that contains all the keys in the collection.
        public String[] AllKeys
        {
            get
            {
                return (this.BaseGetAllKeys());
            }
        }

        // Gets an Object array that contains all the values in the collection.
        public Array AllValues
        {
            get
            {
                return (this.BaseGetAllValues());
            }
        }

        // Gets a String array that contains all the values in the collection.
        public String[] AllStringValues
        {
            get
            {
                return ((String[])this.BaseGetAllValues(typeof(string)));
            }
        }

        // Gets a value indicating if the collection contains keys that are not null.
        public Boolean HasKeys
        {
            get
            {
                return (this.BaseHasKeys());
            }
        }

        // Adds an entry to the collection.
        public void Add(String key, Object value)
        {
            this.BaseAdd(key, value);
        }

        // Removes an entry with the specified key from the collection.
        public void Remove(String key)
        {
            this.BaseRemove(key);
        }

        // Removes an entry in the specified index from the collection.
        public void Remove(int index)
        {
            this.BaseRemoveAt(index);
        }

        // Clears all the elements in the collection.
        public void Clear()
        {
            this.BaseClear();
        }

    }


}
