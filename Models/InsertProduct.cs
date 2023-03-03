using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class InsertProduct
    {
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
