using QuickLearn.Demo.XmlUtility.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuickLearn.Demo.XmlUtility
{
    public class MessageTypeManager
    {

        ISchemaStore schemaStore = null;

        public MessageTypeManager()
        {

        }

        public MessageTypeManager(ISchemaStore schemaStore)
        {
            this.schemaStore = schemaStore;
        }
        
        public Dictionary<string, XmlPropertyExtractor> KnownMessageTypes { get; set; }
            = new Dictionary<string, XmlPropertyExtractor>();

        public async Task LoadSchemasFromStoreAsync()
        {
            if (schemaStore == null) return;

            await (await schemaStore.GetSchemaListAsync())
                  .ForEachAsync(8,
                    async schema => RegisterSchema(
                            await schemaStore.GetSchemaAsync(schema)));
            
            return;
        }

        public void RegisterSchema(string schemaContent)
        {
            var xmlPropertyExtractor = new XmlPropertyExtractor(schemaContent);
            
            KnownMessageTypes[xmlPropertyExtractor.MessageType] = xmlPropertyExtractor;
        }

        public PropertyBag ExtractProperties(string messageInstance)
        {
            var instance = XDocument.Parse(messageInstance);

            var messageType = instance.GetMessageType();

            if (!KnownMessageTypes.ContainsKey(messageType))
                throw new UnrecognizedMessageTypeException();

            return KnownMessageTypes[messageType].GetPropertyBag(instance);
        }

    }
}
