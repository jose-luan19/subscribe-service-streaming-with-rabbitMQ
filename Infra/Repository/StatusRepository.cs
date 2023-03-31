using desafioBack.Infra;
using Models;

namespace Infra.Repository
{
    public class StatusRepository : ARepository<Status>
    {
        public StatusRepository(DbContextClass context) : base(context)
        {
        }
    }
}
