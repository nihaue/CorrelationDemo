using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace QuickLearn.Demo.XmlUtility
{
    public class AzureBlobSchemaStore : ISchemaStore
    {

        internal static MemoryCache ResourceCache = new MemoryCache("SchemaResources");

        private Func<HttpClient> httpClientFactory = null;

        private const string RESOURCE_URL_XPATH = "//Url";
        private const string LISTING_URL_FORMAT = "{0}?restype=container&comp=list";
        private const string LIST_KEY = "SCHEMA_LIST";

        

        public string ContainerUrl { get; set; }
        
        public AzureBlobSchemaStore(string containerUrl, Func<HttpClient> httpClientFactory = null)
        {
            ContainerUrl = containerUrl;
            this.httpClientFactory = httpClientFactory == null ? () => new HttpClient() : httpClientFactory;
        }

        public async Task<string> GetSchemaAsync(string schemaName)
        {
            object cachedSchema = null;

            if (null != (cachedSchema = ResourceCache.Get(schemaName)))
                return cachedSchema as string;

            using (HttpClient client = httpClientFactory())
            {
                var schemaContent = await client.GetStringAsync(schemaName);

                ResourceCache.Add(schemaName, schemaContent,
                    new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30) });

                return schemaContent;
            }
        }

        public async Task<string[]> GetSchemaListAsync()
        {
            object cachedListing = null;

            if (null != (cachedListing = ResourceCache.Get(LIST_KEY)))
                return cachedListing as string[];

            using (HttpClient client = httpClientFactory())
            {
                Uri listingUri = new Uri(string.Format(LISTING_URL_FORMAT, ContainerUrl));

                var listingContent = XDocument.Parse(await client.GetStringAsync(listingUri));

                var listing = (from url in listingContent.XPathSelectElements(RESOURCE_URL_XPATH)
                                where !string.IsNullOrWhiteSpace(url.Value)
                                    && url.Value.EndsWith(".xsd", true, CultureInfo.InvariantCulture)
                                select url.Value).ToArray();

                ResourceCache.Add(LIST_KEY, listing,
                    new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(5)});

                return listing;

            }
        }
    }
}
