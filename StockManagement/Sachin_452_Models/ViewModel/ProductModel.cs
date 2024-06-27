using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Models.ViewModel
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string productName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductType { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
    }
}
