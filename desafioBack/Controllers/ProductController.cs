using desafioBack.RabitMQ;
using desafioBack.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace desafioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProductController(IProductService _productService, IRabitMQProducer rabitMQProducer)
        {
            productService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpPost("SUBSCRIPTION_PURCHASED")]
        public User AddProduct(User user)
        {
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabitMQProducer.SendProductMessage(user);
            return user;
        }
        [HttpPut("SUBSCRIPTION_CANCELED")]
        public Subscription UpdateProduct(Subscription subscription)
        {
            return productService.UpdateProduct(subscription);
        }

        //[HttpPut("SUBSCRIPTION_RESTARTED")]
        //public Product UpdateProduct(InsertProduct insertProduct)
        //{
        //    return productService.UpdateProduct(product);
        //}
    }
}