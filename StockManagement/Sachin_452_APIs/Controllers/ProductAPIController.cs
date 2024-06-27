using Sachin_452_Models.ViewModel;
using Sachin_452_Repository.Interface;
using Sachin_452_Repository.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;

namespace Sachin_452_APIs.Controllers
{
    public class ProductAPIController : ApiController
    {
        IAdminInterface _adminInterface = new AdminService();
        // GET: ProductAPI


        [System.Web.Http.HttpGet] 
        [System.Web.Http.Route("api/ProductAPI/EditProduct")]
        public ProductModel EditProduct(int ProductId)
        {
            try
            {
                ProductModel products = _adminInterface.getProductDetails(ProductId);
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}