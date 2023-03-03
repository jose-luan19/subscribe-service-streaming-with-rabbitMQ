namespace desafioBack.Models
{
    public class Product
    {
        public Guid ProductId
        {
            get;
            set;
        } = Guid.NewGuid();
        public string ProductName
        {
            get;
            set;
        }
        public string ProductDescription
        {
            get;
            set;
        }
        public int ProductPrice
        {
            get;
            set;
        }
        public int ProductStock
        {
            get;
            set;
        }
    }
}