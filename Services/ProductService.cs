
using desafioBack.Infra;
using Models;

namespace desafioBack.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;
        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        
        public User AddProduct(User user)
        {
            var result = _dbContext.User.Add(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }
        public Subscription UpdateProduct(Subscription subscription)
        {
            var result = _dbContext.Subscription.Update(subscription);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}