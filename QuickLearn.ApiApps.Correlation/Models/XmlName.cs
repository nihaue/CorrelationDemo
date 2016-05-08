namespace QuickLearn.Demo.Models
{
    public class XmlName
    {

        public XmlName()
        {

        }

        public XmlName(string fullName)
        {
            FullName = fullName;

            if (!fullName.Contains("#")) return;

            DisplayName = $"{fullName.Split('#')[1]} ({fullName.Split('#')[0]})";
        }

        public string DisplayName { get; set; }
        
        public string FullName { get; set; }
        
        public override bool Equals(object obj)
        {
            return null != obj
                && null != obj as XmlName
                && 0 == string.Compare(FullName, (obj as XmlName).FullName);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}