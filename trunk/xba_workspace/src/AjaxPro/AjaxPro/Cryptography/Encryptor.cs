﻿namespace AjaxPro.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    public class Encryptor
    {
        private byte[] encKey;
        private byte[] initVec;
        private EncryptTransformer transformer;

        public Encryptor(EncryptionAlgorithm algId)
        {
            this.transformer = new EncryptTransformer(algId);
        }

        public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
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
            this.encKey = this.transformer.Key;
            this.initVec = this.transformer.IV;
            stream2.FlushFinalBlock();
            stream2.Close();
            return stream.ToArray();
        }

        public byte[] IV
        {
            get
            {
                return this.initVec;
            }
            set
            {
                this.initVec = value;
            }
        }

        public byte[] Key
        {
            get
            {
                return this.encKey;
            }
        }
    }
}

