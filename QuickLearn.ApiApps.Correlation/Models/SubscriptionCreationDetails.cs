using Newtonsoft.Json.Linq;
using TRex.Metadata;

namespace QuickLearn.Demo.Models
{
    public class SubscriptionCreationDetails
    {

        [Metadata("Message Properties")]
        public JToken Properties { get; set; }

        [Metadata("Service Bus Topic")]
        public string MessageBoxTopic { get; set; }

        [Metadata("Service Bus Connection String")]
        public string ServiceBusConnectionString { get; set; }
    }
}