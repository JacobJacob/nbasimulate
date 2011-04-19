namespace AjaxPro.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class Decryptor
    {
        private byte[] initVec;
        private DecryptTransformer transformer;

        public Decryptor(EncryptionAlgorithm algId)
        {
            this.transformer = new DecryptTransformer(algId);
        }

        public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
        {
            MemoryStream stream = new MemoryStream();
            this.transformer.IV = this.initVec;
            ICryptoTransform cryptoServiceProvider = this.transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream stream2 = new CryptoStream(stream, cryptoServiceProvider, CryptoStreamMode.Write);
            try
            {
                stream2.Write(bytesData, 0, bytesData.Length);
            }
            catch (Exception exception)
            {
                throw new Exception("Error while writing encrypted data to the stream: \n" + exception.Message);
            }
            stream2.FlushFinalBlock();
            stream2.Close();
            return stream.ToArray();
        }

        public byte[] IV
        {
            set
            {
                this.initVec = value;
            }
        }
    }
}

