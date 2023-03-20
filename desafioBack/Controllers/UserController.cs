using desafioBack.RabitMQ;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace desafioBack.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRabitMQProducer _rabitMQProducer;
        public UserController(IRabitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpPost("SUBSCRIPTION_PURCHASED")]
        public async Task<ActionResult> AddSub(UserInsert userInsert)
        {

            var user = new User()
            {
                FullName = userInsert.FullName,
            };

            _rabitMQProducer.SendProductMessage(user);
            return Ok();
        }
        [HttpPut("SUBSCRIPTION_CANCELED/{id}")]
        public async Task<ActionResult> CanceledSub(Guid id)
        {
            _rabitMQProducer.SendProductMessage(id);
            return Ok();
        }

        [HttpPut("SUBSCRIPTION_RESTARTED/{id}")]
        public async Task<ActionResult> RestartedSub(Guid id)
        {
            _rabitMQProducer.SendProductMessage(id);
            return Ok();

        }
    }
}