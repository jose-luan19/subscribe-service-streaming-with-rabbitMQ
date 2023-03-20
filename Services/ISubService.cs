using Models;

namespace desafioBack.Services
{
    public interface ISubService
    {

        public User AddProduct(User user);
        public void CanceledProduct(Guid id);
        public void RestartedProduct(Guid id);

    }
}