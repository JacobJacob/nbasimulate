namespace AjaxPro
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.InteropServices;

    public class IJavaScriptConverter
    {
        protected bool m_AllowInheritance = false;
        protected Type[] m_deserializableTypes = new Type[0];
        protected Type[] m_serializableTypes = new Type[0];

        public virtual object Deserialize(IJavaScriptObject o, Type t)
        {
            return null;
        }

        public virtual string GetClientScript()
        {
            return "";
        }

        public virtual void Initialize(StringDictionary d)
        {
        }

        public virtual string Serialize(object o)
        {
            throw new NotImplementedException("Converter for type '" + o.GetType().FullName + "'.");
        }

        public virtual bool TryDeserializeValue(IJavaScriptObject jso, Type t, out object o)
        {
            if (this.m_AllowInheritance)
            {
                for (int i = 0; i < this.m_deserializableTypes.Length; i++)
                {
                    if (this.m_deserializableTypes[i].IsAssignableFrom(t))
                    {
                        o = this.Deserialize(jso, t);
                        return true;
                    }
                }
            }
            o = null;
            return false;
        }

        public virtual bool TrySerializeValue(object o, Type t, out string json)
        {
            if (this.m_AllowInheritance)
            {
                for (int i = 0; i < this.m_serializableTypes.Length; i++)
                {
                    if (this.m_serializableTypes[i].IsAssignableFrom(t))
                    {
                        json = this.Serialize(o);
                        return true;
                    }
                }
            }
            json = null;
            return false;
        }

        public virtual string ConverterName
        {
            get
            {
                return base.GetType().Name;
            }
        }

        public virtual Type[] DeserializableTypes
        {
            get
            {
                return this.m_deserializableTypes;
            }
        }

        public virtual Type[] SerializableTypes
        {
            get
            {
                return this.m_serializableTypes;
            }
        }
    }
}

