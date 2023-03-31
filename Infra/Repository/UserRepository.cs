using desafioBack.Infra;
using Models;

namespace Infra.Repository
{
    public class UserRepository : ARepository<User>
    {
        public UserRepository(DbContextClass context) : base(context)
        {
        }
    }
}
