namespace Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}
