using desafioBack.Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace desafioBack.Services
{
    public class SubService : ISubService
    {
        private UserRepository _userRepository;
        private StatusRepository _statusRepository;
        private SubscriptionRepository _subscriptionRepository;
        private EventHistoryRepository _eventHistoryRepository;


        public async Task<User> AddSubAsync(User user, DbContextClass _context)
        {

            NewRepositorys(_context);

            var status = await _statusRepository.
                                Queryable()
                                .Where(x => x.Id == Guid.Parse("c35834ae-6acf-45d8-9f75-95ff9035bee3"))
                                .FirstOrDefaultAsync();

            var sub = new Subscription
            {
                User = user,
                UserId = user.Id,
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
            status.Subscription = sub;


            _userRepository.Insert(user);
            _subscriptionRepository.Insert(sub);
            _eventHistoryRepository.Insert(eventHistory);

            _userRepository.Commit();
            _subscriptionRepository.Commit();
            _eventHistoryRepository.Commit();

            return user;
        }

        private void NewRepositorys(DbContextClass context)
        {
            _userRepository = new UserRepository(context);
            _subscriptionRepository = new SubscriptionRepository(context);
            _statusRepository = new StatusRepository(context);
            _eventHistoryRepository = new EventHistoryRepository(context);
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