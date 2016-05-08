using TRex.Metadata;

namespace QuickLearn.ApiApps.Correlation.Models
{
    public class CreatedInstanceSubscription
    {
        [Metadata("Instance Subscription Name", "Name of the service bus subscription created for this instance of the Logic App", VisibilityType.Important)]
        public string SubscriptionName { get; set; }

        [Metadata("Topic Name", "Name of the topic under which the subscription was created", VisibilityType.Important)]
        public string TopicName { get; set; }

        [Metadata("Service Bus Connection", Visibility = VisibilityType.Important)]
        public string ServiceBusConnection { get; set; }
    }
}