
using desafioBack.Infra;
using Models;

namespace desafioBack.Services
{
    public class SubService : ISubService
    {
        private readonly DbContextClass _dbContext;
        public SubService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public User AddProduct(User user)
        {
            var result = _dbContext.User.Add(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public void CanceledProduct(Guid id)
        {
            //var result = _dbContext.Subscription.Update(subscription);
            //_dbContext.SaveChanges();
            //return result.Entity;
        }
        public void RestartedProduct(Guid id)
        {
            //var result = _dbContext.Subscription.Update(subscription);
            //_dbContext.SaveChanges();
            //return result.Entity;
        }
    }
}