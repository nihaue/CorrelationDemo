using TRex.Metadata;

namespace QuickLearn.Demo.Models
{
    public class PropertyPromotionRequest
    {
        [Metadata("Schema Blob Storage Container")]
        public string DocumentSchemaRootUrl { get; set; }

        [Metadata("Xml Message Content")]
        public string XmlContent { get; set; }
    }
}