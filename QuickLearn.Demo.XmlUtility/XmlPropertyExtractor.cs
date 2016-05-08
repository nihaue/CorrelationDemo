using QuickLearn.Demo.XmlUtility.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace QuickLearn.Demo.XmlUtility
{
    public class XmlPropertyExtractor
    {
        
        private const string PROPERTY_NODE = @"//*[local-name()='property' and namespace-uri()='http://schemas.microsoft.com/BizTalk/2003']";
        private const string NAMESPACE_NODE = @"//*[local-name()='namespace' and namespace-uri()='http://schemas.microsoft.com/BizTalk/2003']";
        private const string TARGET_NAMESPACE = @"string(//*[local-name()='schema']/@targetNamespace)";
        private const string ROOT_NODE_NAME = @"string(//*[local-name()='element'][1]/@name)";

        public XmlPropertyExtractor(string schemaContent) : this(XDocument.Parse(schemaContent)) { }

        public XmlPropertyExtractor(XDocument schema)
        {
            this.Schema = schema;
        }

        public PropertyBag GetPropertyBag(XDocument instance)
        {

            var result = new Dictionary<string, object>();

            if (instance == null) return PropertyBag.FromDictionary(result);

            result.Add(SystemProperties.MessageType, instance.GetMessageType());

            if (Properties == null || Properties.Length == 0) return PropertyBag.FromDictionary(result);

            foreach (var property in Properties)
            {
                var matchingNode = (instance as XNode).XPathSelectElement(property.XPath);
                if (null != matchingNode)
                {
                    result.Add(property.FullName, matchingNode.Value);
                }
            }

            return PropertyBag.FromDictionary(result);
        }
        
        public XDocument Schema { get; private set; }

        private string messageType;
        public string MessageType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(messageType)) return messageType;

                if (Schema == null) return null;

                return messageType = string.Format("{0}#{1}",
                    Schema.XPathEvaluate(TARGET_NAMESPACE),
                    Schema.XPathEvaluate(ROOT_NODE_NAME));
            }
        }

        private PromotedProperty[] properties;

        public PromotedProperty[] Properties

        {
            get
            {
                if (properties != null) return properties;

                if (Schema == null) return null;

                var namespaces = (from n in Schema.XPathSelectElements(NAMESPACE_NODE)
                                  let prefixNode = n.Attributes("prefix").FirstOrDefault()
                                  let prefix = prefixNode == null ? null : prefixNode.Value
                                  let uriNode = n.Attributes("uri").FirstOrDefault()
                                  let uri = uriNode == null ? null : uriNode.Value
                                  select new
                                  {
                                      Prefix = prefix,
                                      Uri = uri
                                  }).ToDictionary(n => n.Prefix, n => n.Uri);

                return properties = (from p in Schema.XPathSelectElements(PROPERTY_NODE)
                                     let qualifiedNameNode = p.Attributes("name").FirstOrDefault()
                                     let qualifiedName = qualifiedNameNode == null
                                                                    ? null
                                                                    : qualifiedNameNode.Value
                                     let parsedName = qualifiedName == null
                                                                    ? new string[] { null, null }
                                                                    : qualifiedName.Contains(":")
                                                                        ? qualifiedName.Split(new char[] { ':' }, 2)
                                                                        : new string[] { null, qualifiedName }
                                     let xpathNode = p.Attributes("xpath").FirstOrDefault()
                                     let xpath = xpathNode == null ? null : xpathNode.Value
                                     select new PromotedProperty()
                                     {
                                         Name = parsedName[1],
                                         Namespace = !namespaces.ContainsKey(parsedName[0]) ? null : namespaces[parsedName[0]],
                                         XPath = xpath
                                     }).ToArray();

            }
        }

    }

}
