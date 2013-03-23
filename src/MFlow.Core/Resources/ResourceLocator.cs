using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;

namespace MFlow.Core.Resources
{
    /// <summary>
    ///     A resource locator implementation
    /// </summary>
    class ResourceLocator : IResourceLocator
    {

        static readonly Dictionary<string, Dictionary<string, string>> _resources;

        /// <summary>
        ///     Static type initializer that loads the default and current culture resources
        /// </summary>
        static ResourceLocator()
        {
            _resources = new Dictionary<string, Dictionary<string, string>>();

            LoadAndCache(string.Format("Messages.{0}.xml", Thread.CurrentThread.CurrentUICulture.Name));
            LoadAndCache("Messages.en.xml");
        }

        /// <summary>
        ///     Gets a resource by key for the loaded cultures, will try to load the current culture again if it hasnt 
        ///     already been loaded
        /// </summary>
        public string GetResource(string key)
        {
            var derivedName = string.Format("Messages.{0}.xml", Thread.CurrentThread.CurrentUICulture.Name);
            var defaultName = "Messages.en.xml";

            if (!_resources.ContainsKey(derivedName))
                LoadAndCache(derivedName);

            Dictionary<string, string> defaultDictionary = null;

            if (_resources.ContainsKey(defaultName))
                defaultDictionary = _resources.Single(r => r.Key == defaultName).Value;

            Dictionary<string, string> derivedDictionary = null;

            if (_resources.ContainsKey(derivedName))
                derivedDictionary = _resources.Single(r => r.Key == derivedName).Value;

            var resource = string.Empty;

            if (defaultDictionary != null && defaultDictionary.Any(i => i.Key == key))
                resource = defaultDictionary.Single(i => i.Key == key).Value;

            if (derivedDictionary != null && derivedDictionary.Any(i => i.Key == key))
                resource = derivedDictionary.Single(i => i.Key == key).Value;

            return resource;
        }

        static void LoadAndCache(string fileName)
        {
            var path = string.Format(@"{0}\Resources\Xml\{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            var customPath = string.Format(@"{0}\Resources\Xml\Custom.{1}", Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), fileName);
            if (!_resources.ContainsKey(path))
            {
                if (File.Exists(path))
                {
                    var document = ParseDocument(LoadDocument(path));

                    if (File.Exists(customPath))
                    {
                        var customDocument = ParseDocument(LoadDocument(customPath));

                        foreach (var item in customDocument)
                            document.Add(item.Key, item.Value);

                    }

                    _resources.Add(fileName, document);

                }
            }
        }

        static XDocument LoadDocument(string fileName)
        {
            var document = XDocument.Load(fileName);
            return document;
        }

        static Dictionary<string, string> ParseDocument(XContainer document)
        {
            var output = new Dictionary<string, string>();
            var nodes = document.Descendants(XName.Get("Messages")).SingleOrDefault().Descendants();

            foreach (var item in nodes)
            {
                output.Add(item.Name.LocalName, item.Value);
            }

            return output;
        }
    }
}
