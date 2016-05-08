namespace QuickLearn.Demo.XmlUtility
{
    public class PromotedProperty
    {
        public string Namespace { get; set; }
        public string Name { get; set; }

        public string XPath { get; set; }
        public string FullName { get
            {
                return string.Format("{0}#{1}", Namespace, Name);
            }
        }
    }
}