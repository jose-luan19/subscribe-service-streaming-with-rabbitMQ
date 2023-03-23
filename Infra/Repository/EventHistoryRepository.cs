using desafioBack.Infra;
using Models;

namespace Infra.Repository
{
    public class EventHistoryRepository : ARepository<EventHistory>
    {
        public EventHistoryRepository(DbContextClass context) : base(context)
        {
        }
    }
}
