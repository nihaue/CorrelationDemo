using Newtonsoft.Json.Linq;
using TRex.Metadata;

namespace QuickLearn.Demo.Models
{
    public class SubscriptionCreationDetails
    {
        [Metadata("Correlation Property", "Property on which to base the instance subscription.")]
        public string CorrelationProperty { get; set; }

        [Metadata("Message Type of Next Message", "Instance subscription will be created to subscribe to this message type only.")]
        public string SubscribedMessageType { get; set; }

        [Metadata("Message Properties")]
        public JToken Properties { get; set; }

        [Metadata("Service Bus Topic")]
        public string MessageBoxTopic { get; set; }

        [Metadata("Service Bus Connection String")]
        public string ServiceBusConnectionString { get; set; }
    }
}