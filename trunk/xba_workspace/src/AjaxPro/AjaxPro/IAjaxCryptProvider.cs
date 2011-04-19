namespace AjaxPro
{
    using System;

    public interface IAjaxCryptProvider
    {
        string Decrypt(string jsoncrypt);
        string Encrypt(string json);

        string ClientScript { get; }

        IAjaxKeyProvider KeyProvider { set; }
    }
}

