using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickLearn.Demo.XmlUtility.Tests.Mocks;
using QuickLearn.Demo.XmlUtility.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickLearn.Demo.XmlUtility.Tests
{
    [TestClass]
    public class PropertyPromotionTests
    {

        [TestCleanup]
        public void ClearCache()
        {
            foreach (var item in AzureBlobSchemaStore.ResourceCache)
                AzureBlobSchemaStore.ResourceCache.Remove(item.Key);
        }

        [TestMethod]
        public void MessageTypeManager_KnownDocument_PropertiesPromoted()
        {
            MessageTypeManager manager = new MessageTypeManager();
            manager.RegisterSchema(XmlStrings.SCHEMA_STRING);

            var properties = manager.ExtractProperties(XmlStrings.DOCUMENT_STRING);

            Assert.AreEqual(2, properties.Properties.Count);
            Assert.AreEqual(documentMessageType, properties.Properties.GetValue(SystemProperties.MessageType));
            Assert.AreEqual("OrderId_0", properties.Properties.GetValue(orderIdProperty));
        }

        [TestMethod]
        public async Task AzureBlobSchemaStore_ValidContainer_ResourcesReturned()
        {
            ISchemaStore schemaStore = new AzureBlobSchemaStore(schemaContainer);
            var schemas = await schemaStore.GetSchemaListAsync();
            Assert.IsNotNull(schemas);
            Assert.AreEqual(2, schemas.Length);
        }

        [TestMethod]
        public async Task AzureBlobSchemaStore_MultipleCalls_SchemasAreLoadedFromCacheOn2ndCall()
        {
            int callCount = 0;
            
            Func<HttpClient> httpClientFactory = () =>
            {
                var fakeHandler = new FakeHttpMessageHandler();
                fakeHandler.FakeHttpClientCalled += (s, e) => ++callCount;

                var client = new HttpClient(fakeHandler);
                return client;
            };
            
            ISchemaStore schemaStore = new AzureBlobSchemaStore(schemaContainer, httpClientFactory);
            
            MessageTypeManager manager = new MessageTypeManager(schemaStore);

            await manager.LoadSchemasFromStoreAsync();

            Assert.AreNotEqual(0, callCount);

            callCount = 0;
            manager.KnownMessageTypes.Clear();
            manager.KnownMessageTypes = null;
            manager.KnownMessageTypes = new Dictionary<string, XmlPropertyExtractor>();

            await manager.LoadSchemasFromStoreAsync();

            Assert.AreEqual(0, callCount);

            Assert.IsNotNull(manager.KnownMessageTypes);
            Assert.AreEqual(1, manager.KnownMessageTypes.Values.Count);
        }


        [TestMethod]
        public async Task Integration_AzureBlobSchemaStore_SchemasAreLoaded()
        {
            ISchemaStore schemaStore = new AzureBlobSchemaStore(schemaContainer);
            MessageTypeManager manager = new MessageTypeManager(schemaStore);

            await manager.LoadSchemasFromStoreAsync();

            Assert.IsNotNull(manager.KnownMessageTypes);
            Assert.AreEqual(2, manager.KnownMessageTypes.Values.Count);
        }

        [TestMethod]
        public async Task Integration_AzureBlobSchemaStore_SchemasCanBeUsedForPromotion()
        {
            ISchemaStore schemaStore = new AzureBlobSchemaStore(schemaContainer);
            MessageTypeManager manager = new MessageTypeManager(schemaStore);
            await manager.LoadSchemasFromStoreAsync();

            var properties = manager.ExtractProperties(XmlStrings.DOCUMENT_STRING);

            Assert.AreEqual(2, properties.Properties.Count);
            Assert.AreEqual("OrderId_0", properties.Properties.GetValue(orderIdProperty));
        }

        [TestMethod]
        public void XmlPropertyExtractor_SimpleDocumentSchemaLoaded_MessageTypeResolved()
        {
            XmlPropertyExtractor extractor = new XmlPropertyExtractor(XmlStrings.SCHEMA_STRING);

            Assert.AreEqual("http://schemas.quicklearn.com/correlation/demo#PrintJob",
                                extractor.MessageType);
        }


        [TestMethod]
        public void XmlPropertyExtractor_XmlSchemaString_XmlSchemaLoadedWithoutError()
        {
            XmlPropertyExtractor extractor = new XmlPropertyExtractor(XmlStrings.SCHEMA_STRING);

            Assert.IsNotNull(extractor.Schema);

        }

        [TestMethod]
        public void XmlPropertyExtractor_SampleXmlSchema_PromotedPropertyResolved()
        {

            XmlPropertyExtractor extractor = new XmlPropertyExtractor(XmlStrings.SCHEMA_STRING);

            Assert.AreEqual(1, extractor.Properties.Length);

            Assert.AreEqual("OrderId", extractor.Properties[0].Name);
            Assert.AreEqual("http://schemas.quicklearn.com/properties/demo", extractor.Properties[0].Namespace);
            Assert.AreEqual("/*[local-name()='PrintJob' and namespace-uri()='http://schemas.quicklearn.com/correlation/demo']/*[local-name()='OrderId' and namespace-uri()='']", extractor.Properties[0].XPath);
        }


        public string documentMessageType = "http://schemas.quicklearn.com/correlation/demo#PrintJob";
        public string schemaContainer = "http://images.quicklearn.com/schemas";
        public string orderIdProperty = "http://schemas.quicklearn.com/properties/demo#OrderId";

    }
}
