using desafioBack.Infra;
using Models;

namespace Infra.Repository
{
    public class SubscriptionRepository : ARepository<Subscription>
    {
        public SubscriptionRepository(DbContextClass context) : base(context)
        {
        }
    }
}
