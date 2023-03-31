using desafioBack.Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace desafioBack.Services
{
    public class SubService : ISubService
    {
        private UserRepository _userRepository;
        private StatusRepository _statusRepository;
        private SubscriptionRepository _subscriptionRepository;
        private EventHistoryRepository _eventHistoryRepository;

        public SubService(DbContextClass context)
        {
            _userRepository = new UserRepository(context);
            _subscriptionRepository = new SubscriptionRepository(context);
            _statusRepository = new StatusRepository(context);
            _eventHistoryRepository = new EventHistoryRepository(context);
        }


        public User CreateSub(User user)
        {

            //NewRepositorys(_context);

            var status = GetStatusInDatabase("c35834ae-6acf-45d8-9f75-95ff9035bee3");

            var sub = new Subscription
            {
                User = user,
                Status = status,
            };

            var eventHistory = new EventHistory
            {
                Subscription = sub,
                Type = "Created"
            };


            _userRepository.Insert(user);
            _subscriptionRepository.Insert(sub);
            _eventHistoryRepository.Insert(eventHistory);

            _userRepository.Commit();
            _subscriptionRepository.Commit();
            _eventHistoryRepository.Commit();

            return user;
        }



        public void CanceledSub(Guid id)
        {
            var sub = SelectSubscription(id);

            var status = GetStatusInDatabase("03f4ec06-8cc0-4e9f-af16-4b40d912fac1");

            var eventCanceled = new EventHistory
            {
                Subscription = sub,
                Type = "Canceled"
            };
            sub.Status = status;
            sub.Update_at = DateTime.Now;

            _eventHistoryRepository.Insert(eventCanceled);
            _subscriptionRepository.Update(sub);


            _eventHistoryRepository.Commit();
            _subscriptionRepository.Commit();
        }
        public void RestartedSub(Guid id)
        {
            var sub = SelectSubscription(id);
            var status = GetStatusInDatabase("c35834ae-6acf-45d8-9f75-95ff9035bee3");

            var eventRestated = new EventHistory
            {
                Subscription= sub,
                Type = "Restarted"
            };
            sub.Status = status;
            sub.Update_at = DateTime.Now;

            _eventHistoryRepository.Insert(eventRestated);
            _subscriptionRepository.Update(sub);


            _eventHistoryRepository.Commit();
            _subscriptionRepository.Commit();

        }

        private Status GetStatusInDatabase(string id)
        {
            var status =  _statusRepository.
                                Queryable()
                                .Where(x => x.Id == Guid.Parse(id))
                                .FirstOrDefault();
            return status;
        }

        private Subscription SelectSubscription(Guid id)
        {

            #pragma warning disable CS8603 // Possível retorno de referência nula.
            return _subscriptionRepository.Queryable().Where(x => x.Id == id).FirstOrDefault();
            #pragma warning restore CS8603 // Possível retorno de referência nula.
        }

    }
}