using desafioBack.Infra;
using Models;

namespace desafioBack.Services
{
    public interface ISubService
    {

        public Task<User> AddSubAsync(User user, DbContextClass _context);
        public void CanceledProduct(Guid id);
        public void RestartedProduct(Guid id);

    }
}