namespace AjaxPro.Cryptography
{
    using System;
    using System.Text;

    public class WebDecrypter
    {
        public string Decrypt(string Text, string Key)
        {
            if (Key.Length != 8)
            {
                throw new Exception("Key must be a 8-bit string!");
            }
            byte[] buffer = null;
            byte[] bytesData = null;
            byte[] bytesKey = null;
            byte[] bytes = null;
            try
            {
                Decryptor decryptor = new Decryptor(EncryptionAlgorithm.Des);
                buffer = Encoding.ASCII.GetBytes("init vec");
                decryptor.IV = buffer;
                bytesKey = Encoding.ASCII.GetBytes(Key);
                bytesData = Convert.FromBase64String(Text);
                bytes = decryptor.Decrypt(bytesData, bytesKey);
            }
            catch (Exception exception)
            {
                throw new Exception("Exception while decrypting. " + exception.Message);
            }
            return Encoding.ASCII.GetString(bytes);
        }
    }
}

