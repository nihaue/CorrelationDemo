using System.Xml.Linq;

namespace QuickLearn.Demo.XmlUtility.Extensions
{
    public static class XDocumentExtensions
    {
        public static string GetMessageType(this XDocument instance)
        {
            return string.Format("{0}#{1}",
                instance.Root.Name.NamespaceName,
                instance.Root.Name.LocalName);
        }

    }
}
