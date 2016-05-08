using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Collections;

namespace QuickLearn.Demo.XmlUtility
{
    public class PropertyBag
    {
        public PropertyBag()
        {
        }

        public JObject Properties { get; set; } = new JObject();

        public static PropertyBag FromDictionary(IDictionary<string, object> source)
        {
            PropertyBag created = new PropertyBag();

            created.Properties = created.Properties ?? (created.Properties = new JObject());

            foreach (var pair in source)
            {
                created.Properties.Add(pair.Key, JToken.FromObject(pair.Value));
            }

            return created;
        }

    }
}
