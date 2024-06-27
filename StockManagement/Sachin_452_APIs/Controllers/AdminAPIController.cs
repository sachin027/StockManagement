using Sachin_452_APIs.JwtAuthentication;
using Sachin_452_Models.DBContext;
using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using Sachin_452_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Sachin_452_APIs.Controllers
{
    public class AdminAPIController : ApiController
    {
        IAdminInterface _adminInterface = new AdminService();
        // GET: AdminAPI

        [HttpGet]
        [Route("api/AdminAPI/ProductList")]

        public List<ProductModel> ProductList()
        {
            try
            {
                List<ProductModel> productList = _adminInterface.GetProductList();
                return productList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("api/AdminAPI/AddProduct")]
        public ProductModel AddProduct(ProductModel productModel)
        {
            try
            {
                ProductModel products = _adminInterface.addProduct(productModel);
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}