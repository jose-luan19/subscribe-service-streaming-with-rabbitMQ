namespace Models
{
    public class EventHistory : BaseEntity
    {
        public Subscription Subscription { get; set; }
        public Guid SubscriptionId { get; set; }
        public string Type { get; set; }
    }
}
