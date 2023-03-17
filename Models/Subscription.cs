using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subscription : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Status Status { get; set; }
        public Guid StatusId { get; set; }
        public EventHistory EventHistory { get; set; }
        public DateTime Update_at { get; set; }
    }
}
