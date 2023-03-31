namespace Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
