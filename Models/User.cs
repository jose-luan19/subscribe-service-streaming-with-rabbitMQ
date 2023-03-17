namespace Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public Subscription Subscription { get; set; }

    }
}
