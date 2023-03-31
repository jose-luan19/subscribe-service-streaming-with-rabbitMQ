namespace Models
{
    public class Subscription : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
        public Guid StatusId { get; set; }
        public List<EventHistory> Histories { get; set; }
        public DateTime Update_at { get; set; }
    }
}
