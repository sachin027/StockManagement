using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Models.ViewModel
{
    public class CartModel
    {
        //public int cartId { get; set; }
        //public Nullable<int> ProductId { get; set; }
        //public Nullable<System.DateTime> orderDate { get; set; }
        //public Nullable<int> quantity { get; set; }
        //public string productType { get; set; }
        //public Nullable<int> price { get; set; }
        //public Nullable<int> userid { get; set; }
        //public string productName { get; set; }
        //public Nullable<int> total { get; set; }


        public int CartId { get; set; }

        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> supplierId { get; set; }
    }
}
