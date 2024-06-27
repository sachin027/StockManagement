using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Models.ViewModel
{
    public class OrderModel
    {
        public int Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

    }
    public class OrderModelList
    {
        public List<OrderModel> _OrderModel;
    }
}
