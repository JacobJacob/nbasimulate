namespace AjaxPro
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Xml;

    internal class AjaxSettingsSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            AjaxSettings settings = new AjaxSettings();
            Utility.AddDefaultConverter(settings);
            foreach (XmlNode node in section.ChildNodes)
            {
                if (node.Name == "coreScript")
                {
                    if ((node.InnerText != null) && (node.InnerText.Length > 0))
                    {
                        settings.ScriptReplacements.Add("core", node.InnerText);
                    }
                    continue;
                }
                if (node.Name == "scriptReplacements")
                {
                    foreach (XmlNode node2 in node.SelectNodes("file"))
                    {
                        string key = "";
                        string innerText = "";
                        if (node2.Attributes["name"] != null)
                        {
                            key = node2.Attributes["name"].InnerText;
                            if (node2.Attributes["path"] != null)
                            {
                                innerText = node2.Attributes["path"].InnerText;
                            }
                            if (settings.ScriptReplacements.ContainsKey(key))
                            {
                                settings.ScriptReplacements[key] = innerText;
                            }
                            else
                            {
                                settings.ScriptReplacements.Add(key, innerText);
                            }
                        }
                    }
                    continue;
                }
                if (node.Name == "urlNamespaceMappings")
                {
                    settings.OnlyAllowTypesInList = node.SelectSingleNode("@allowListOnly[.='true']") != null;
                    settings.UseAssemblyQualifiedName = node.SelectSingleNode("@useAssemblyQualifiedName[.='true']") != null;
                    foreach (XmlNode node5 in node.SelectNodes("add"))
                    {
                        XmlNode node3 = node5.SelectSingleNode("@type");
                        XmlNode node4 = node5.SelectSingleNode("@path");
                        if (((node3 != null) && (node3.InnerText.Length != 0)) && ((node4 != null) && (node4.InnerText.Length != 0)))
                        {
                            if (settings.UrlNamespaceMappings.Contains(node4.InnerText))
                            {
                                throw new Exception("Duplicate namespace mapping '" + node4.InnerText + "'.");
                            }
                            settings.UrlNamespaceMappings.Add(node4.InnerText, node3.InnerText);
                        }
                    }
                    continue;
                }
                if (node.Name == "jsonConverters")
                {
                    foreach (XmlNode node6 in node.SelectNodes("add"))
                    {
                        XmlNode node7 = node6.SelectSingleNode("@type");
                        if (node7 != null)
                        {
                            Type c = Type.GetType(node7.InnerText);
                            if ((c != null) && typeof(IJavaScriptConverter).IsAssignableFrom(c))
                            {
                                StringDictionary d = new StringDictionary();
                                foreach (XmlAttribute attribute in node6.Attributes)
                                {
                                    if (!d.ContainsKey(attribute.Name))
                                    {
                                        d.Add(attribute.Name, attribute.Value);
                                    }
                                }
                                IJavaScriptConverter converter = (IJavaScriptConverter) Activator.CreateInstance(c);
                                converter.Initialize(d);
                                Utility.AddConverter(settings, converter, true);
                            }
                        }
                    }
                    foreach (XmlNode node8 in node.SelectNodes("remove"))
                    {
                        XmlNode node9 = node8.SelectSingleNode("@type");
                        if (node9 != null)
                        {
                            Type type = Type.GetType(node9.InnerText);
                            if (type != null)
                            {
                                Utility.RemoveConverter(settings, type);
                            }
                        }
                    }
                    continue;
                }
                if (node.Name == "encryption")
                {
                    string cryptType = (node.SelectSingleNode("@cryptType") != null) ? node.SelectSingleNode("@cryptType").InnerText : null;
                    string keyType = (node.SelectSingleNode("@keyType") != null) ? node.SelectSingleNode("@keyType").InnerText : null;
                    if ((cryptType != null) && (keyType != null))
                    {
                        AjaxEncryption encryption = new AjaxEncryption(cryptType, keyType);
                        if (!encryption.Init())
                        {
                            throw new Exception("Ajax.NET Professional encryption configuration failed.");
                        }
                        settings.Encryption = encryption;
                    }
                    continue;
                }
                if (node.Name == "token")
                {
                    settings.TokenEnabled = (node.SelectSingleNode("@enabled") != null) && (node.SelectSingleNode("@enabled").InnerText == "true");
                    settings.TokenSitePassword = (node.SelectSingleNode("@sitePassword") != null) ? node.SelectSingleNode("@sitePassword").InnerText : settings.TokenSitePassword;
                }
                else
                {
                    if (node.Name == "debug")
                    {
                        if ((node.SelectSingleNode("@enabled") != null) && (node.SelectSingleNode("@enabled").InnerText == "true"))
                        {
                            settings.DebugEnabled = true;
                        }
                        continue;
                    }
                    if (node.Name == "oldStyle")
                    {
                        foreach (XmlNode node10 in node.ChildNodes)
                        {
                            settings.OldStyle.Add(node10.Name);
                        }
                        continue;
                    }
                }
            }
            return settings;
        }
    }
}

