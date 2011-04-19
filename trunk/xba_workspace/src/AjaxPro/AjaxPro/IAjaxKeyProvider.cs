namespace AjaxPro
{
    using System;

    public interface IAjaxKeyProvider
    {
        string ClientScript { get; }

        string Key { get; }
    }
}

