using Microsoft.AspNetCore.Mvc;
using desafioBack.Models;
using desafioBack.RabitMQ;
using desafioBack.Services;
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
        [HttpGet("productlist")]
        public IEnumerable<Product> ProductList()
        {
            var productList = productService.GetProductList();
            return productList;
        }
        [HttpGet("getproductbyid")]
        public Product GetProductById(Guid Id)
        {
            return productService.GetProductById(Id);
        }
        [HttpPost("addproduct")]
        public Product AddProduct(InsertProduct insertProduct)
        {
            var product = new Product
            {
                ProductDescription = insertProduct.ProductDescription,
                ProductName = insertProduct.ProductName,
                ProductPrice = insertProduct.ProductPrice,
                ProductStock = insertProduct.ProductStock
            };
            var productData = productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabitMQProducer.SendProductMessage(productData);
            return productData;
        }
        [HttpPut("updateproduct")]
        public Product UpdateProduct(InsertProduct insertProduct)
        {
            var product = new Product
            {
                ProductDescription = insertProduct.ProductDescription,
                ProductName = insertProduct.ProductName,
                ProductPrice = insertProduct.ProductPrice,
                ProductStock = insertProduct.ProductStock
            };

            return productService.UpdateProduct(product);
        }
        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(Guid Id)
        {
            return productService.DeleteProduct(Id);
        }
    }
}