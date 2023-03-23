
using desafioBack.Infra;
using Infra.Repository;
using Models;
using System.Security.Cryptography.X509Certificates;

namespace desafioBack.Services
{
    public class SubService : ISubService
    {
        private readonly IARepository<User> _userRepository;
        private readonly IARepository<Status> _statusRepository;
        private readonly IARepository<Subscription> _subscriptionRepository;
        private readonly IARepository<EventHistory> _eventHistoryRepository;
        public SubService
            (
            IARepository<User> userRepository,
            IARepository<Status> statusRepository,
            IARepository<Subscription> subscriptionRepository,
            IARepository<EventHistory> eventHistoryRepository
            )
        {
            _userRepository = userRepository;
            _statusRepository = statusRepository;
            _subscriptionRepository = subscriptionRepository;
            _eventHistoryRepository = eventHistoryRepository;
        }

        public  User AddSub(User user)
        {
            var status = _statusRepository.Queryable().Where(x => x.Id == Guid.Parse("c35834ae-6acf-45d8-9f75-95ff9035bee3")).ToList().First();

            var sub = new Subscription 
            { 
                User = user,
                UserId= user.Id,
                Status = status,
                StatusId = status.Id,
            };

            var eventHistory = new EventHistory
            {
                Subscription = sub,
                SubscriptionId = sub.Id,
                Type = "Created"
            };

            sub.EventHistory = eventHistory;
            user.Subscription = sub;
            status.Subscription= sub;


            _userRepository.Insert(user);
            _subscriptionRepository.Insert(sub);
            _eventHistoryRepository.Insert(eventHistory);

            _userRepository.Commit();
            _subscriptionRepository.Commit();
            _eventHistoryRepository.Commit();

            return user;
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