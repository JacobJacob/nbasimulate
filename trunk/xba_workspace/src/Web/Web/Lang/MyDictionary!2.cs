namespace Web.Lang
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Serializable]
    public class MyDictionary<Type, Value> : Dictionary<Type, Value>
    {
        public static MyDictionary<Type, Value> Parse(object obj)
        {
            if (!(obj is Dictionary<Type, Value>))
            {
                return null;
            }
            Dictionary<Type, Value> dictionary = obj as Dictionary<Type, Value>;
            MyDictionary<Type, Value> dictionary2 = new MyDictionary<Type, Value>();
            lock (dictionary)
            {
                foreach (Type local in dictionary.Keys)
                {
                    dictionary2[local] = dictionary[local];
                }
            }
            return dictionary2;
        }

        public static MyDictionary<Type, Value>[] Parse(object[] objs)
        {
            MyDictionary<Type, Value>[] dictionaryArray = new MyDictionary<Type, Value>[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                dictionaryArray[i] = MyDictionary<Type, Value>.Parse(objs[i]);
            }
            return dictionaryArray;
        }

        public Value this[Type key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    string.Format("MyDictionary {0} 键值不存在", key);
                }
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}

