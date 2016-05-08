using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using QuickLearn.ApiApps.Correlation.Models;
using QuickLearn.Demo.Models;
using QuickLearn.Demo.XmlUtility;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TRex.Metadata;

namespace QuickLearn.ApiApps.Correlation.Controllers
{
    [RoutePrefix("correlation")]
    public class CorrelationController : ApiController
    {


        [Route("subscription"), HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Instance Subscription Information", typeof(CreatedInstanceSubscription))]
        [Metadata("Create Instance Subscription", "Subscribes for a message with properties that correlate to known message properties for this Logic App instance", VisibilityType.Important)]
        public async Task<IHttpActionResult> CreateInstanceSubscription(
                    [Metadata("Schema Blob Storage Container")]
                    string documentSchemaRootUrl,
                    [DynamicValueLookup(LookupOperation = "GetProperties",
                                        Parameters = "documentSchemaRootUrl={documentSchemaRootUrl}",
                                        ValuePath = "FullName",
                                        ValueTitle = "Name")]
                    [Metadata("Correlation Property")]
                    string correlationProperty,

                    [DynamicValueLookup(LookupOperation = "GetMessageTypes",
                                        Parameters = "documentSchemaRootUrl={documentSchemaRootUrl}",
                                        ValuePath = "FullName",
                                        ValueTitle = "Name")]
                    [Metadata("Subscribed Message Type")]
                    string subscribedMessageType,

                    [FromBody]SubscriptionCreationDetails subscriptionCreationDetails)
        {

            string instanceSubscriptionId = await createSubscription(
                subscriptionCreationDetails.ServiceBusConnectionString,
                subscriptionCreationDetails.MessageBoxTopic,
                subscribedMessageType,
                correlationProperty,
                subscriptionCreationDetails.Properties.Value<string>(correlationProperty));

            // Using Ok instead of Created since the resource is not readily addressable
            return Ok(new CreatedInstanceSubscription()
            {
                ServiceBusConnection = subscriptionCreationDetails.ServiceBusConnectionString,
                SubscriptionName = instanceSubscriptionId,
                TopicName = subscriptionCreationDetails.MessageBoxTopic
            });

        }

        [Route("subscription"), HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, "Subscription Deleted")]
        [Metadata("Delete Instance Subscription", "Removes an instance subscription for this Logic App instance", VisibilityType.Important)]
        public async Task<IHttpActionResult> DeleteInstanceSubscription(
            CreatedInstanceSubscription instanceSubscription)
        {
            var manager = NamespaceManager.CreateFromConnectionString(instanceSubscription.ServiceBusConnection);

            await manager.DeleteSubscriptionAsync(instanceSubscription.TopicName, instanceSubscription.SubscriptionName);

            return Ok();
        }

        private static async Task<string> createSubscription(string serviceBusConnectionString, string topic,
            string subscribedMessageType,
            string propertyName,
            string value)
        {
            string subscriptionId = Guid.NewGuid().ToString("N");

            var manager = NamespaceManager.CreateFromConnectionString(serviceBusConnectionString);

            await manager.CreateSubscriptionAsync(topic, subscriptionId,
                new SqlFilter($"[{SystemProperties.MessageType}] = '{subscribedMessageType}' AND [{propertyName}] = '{value}'"));

            return subscriptionId;
        }

        // These are utility operations provided only for resource resolution

        [Route("properties"), HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Known promoted properties", typeof(XmlName[]))]
        [Metadata("Get Properties", Visibility = VisibilityType.Internal)]
        public async Task<IHttpActionResult> GetProperties(string documentSchemaRootUrl)
        {
            AzureBlobSchemaStore store = new AzureBlobSchemaStore(documentSchemaRootUrl);

            MessageTypeManager manager = new MessageTypeManager(store);

            await manager.LoadSchemasFromStoreAsync();

            var allKnownProperties = (from s in manager.KnownMessageTypes.Values
                                      from p in s.Properties
                                      select new XmlName(p.FullName)
                                      ).Distinct().ToArray();

            return Ok(allKnownProperties);
        }

        [Route("schemas"), HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Known schemas", typeof(XmlName[]))]
        [Metadata("Get Message Types", Visibility = VisibilityType.Internal)]
        public async Task<IHttpActionResult> GetMessageTypes(string documentSchemaRootUrl)
        {
            AzureBlobSchemaStore store = new AzureBlobSchemaStore(documentSchemaRootUrl);

            MessageTypeManager manager = new MessageTypeManager(store);

            await manager.LoadSchemasFromStoreAsync();

            var allMessageTypes = (from s in manager.KnownMessageTypes.Values
                                   select new XmlName(s.MessageType)
                                   ).Distinct().ToArray();

            return Ok(allMessageTypes);
        }


    }
}
