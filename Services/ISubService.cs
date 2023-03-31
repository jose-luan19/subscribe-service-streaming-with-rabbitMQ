using Models;

namespace desafioBack.Services
{
    public interface ISubService
    {

        public User CreateSub(User user);
        public void CanceledSub(Guid id);
        public void RestartedSub(Guid id);

    }
}