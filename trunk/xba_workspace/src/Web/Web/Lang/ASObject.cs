namespace Web.Lang
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Serializable]
    public class ASObject : MyDictionary<string, object>
    {
        public int GetInt(string key)
        {
            return Convert.ToInt32(this[key]);
        }

        public string GetString(string key)
        {
            return Convert.ToString(this[key]);
        }

        public ASObject Merge(ASObject aso, bool isMergeSameKey, int sameKeyValueWeight)
        {
            return Merge(this, aso, isMergeSameKey, sameKeyValueWeight);
        }

        public static ASObject Merge(ASObject aso1, ASObject aso2, bool isMergeSameKey, int sameKeyValueWeight)
        {
            if ((isMergeSameKey && (sameKeyValueWeight != 1)) && (sameKeyValueWeight != 2))
            {
                throw new Exception("ASObject.Merge's arg:sameKeyValueWeight must 1 or 2, but the preValue : " + sameKeyValueWeight);
            }
            ASObject obj2 = aso1;
            lock (aso2)
            {
                foreach (string str in aso2.Keys)
                {
                    if (!obj2.ContainsKey(str))
                    {
                        obj2[str] = aso2[str];
                    }
                    else
                    {
                        if (!isMergeSameKey)
                        {
                            throw new Exception("ASObject.Merge's arg:isMergeSameKey is false, but contains same key between to aso");
                        }
                        if ((sameKeyValueWeight != 1) && (sameKeyValueWeight == 2))
                        {
                            obj2[str] = aso2[str];
                        }
                    }
                }
            }
            return obj2;
        }

        public static ASObject Parse(object obj)
        {
            if (!(obj is Dictionary<string, object>))
            {
                return null;
            }
            Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
            ASObject obj2 = new ASObject();
            lock (dictionary)
            {
                foreach (string str in dictionary.Keys)
                {
                    obj2[str] = dictionary[str];
                }
            }
            return obj2;
        }

        public static ASObject[] Parse(object[] objs)
        {
            ASObject[] objArray = new ASObject[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                objArray[i] = Parse(objs[i]);
            }
            return objArray;
        }

        public object this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    Console.WriteLine(string.Format("ASObject {0} 键值不存在", key));
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

