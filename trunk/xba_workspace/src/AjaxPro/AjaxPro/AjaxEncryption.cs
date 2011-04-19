namespace AjaxPro
{
    using System;

    internal class AjaxEncryption
    {
        private IAjaxCryptProvider m_CryptProvider = null;
        private string m_CryptType = null;
        private IAjaxKeyProvider m_KeyProvider = null;
        private string m_KeyType = null;

        internal AjaxEncryption(string cryptType, string keyType)
        {
            this.m_CryptType = cryptType;
            this.m_KeyType = keyType;
        }

        internal bool Init()
        {
            bool flag;
            try
            {
                this.m_CryptProvider = (IAjaxCryptProvider) Activator.CreateInstance(Type.GetType(this.m_CryptType), new object[0]);
                this.m_KeyProvider = (IAjaxKeyProvider) Activator.CreateInstance(Type.GetType(this.m_KeyType), new object[0]);
                this.m_CryptProvider.KeyProvider = this.m_KeyProvider;
                flag = true;
            }
            catch (Exception)
            {
            }
            return flag;
        }

        public IAjaxCryptProvider CryptProvider
        {
            get
            {
                return this.m_CryptProvider;
            }
        }

        public IAjaxKeyProvider KeyProvider
        {
            get
            {
                return this.m_KeyProvider;
            }
        }
    }
}

